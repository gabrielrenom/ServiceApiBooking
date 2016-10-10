using log4net.Repository.Hierarchy;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Web.Models;

namespace Web.Controllers
{
    public class PaypalController : Controller
    {
        //
        // GET: /Paypal/

        public ActionResult Index()
        {

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> PaymentWithCreditCard(PaymentView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    APIContext apiContext = Configuration.GetAPIContext();
                    Payment createdPayment = ToPaymentModel(model).Create(apiContext);

                    if (createdPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
                catch (PayPal.PayPalException ex)
                {
                    return View("FailureView");
                }
            }
            return View("SuccessView");
        }

        public ActionResult PaymentWithPaypal()
        {
            //getting the apiContext as earlier
            APIContext apiContext = Configuration.GetAPIContext();

            try
            {
                string payerId = Request.Params["PayerID"];

                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist

                    //it is returned by the create function call of the payment class

                    // Creating a payment

                    // baseURL is the url on which paypal sendsback the data.

                    // So we have provided URL of this controller only

                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Paypal/PaymentWithPayPal?";

                    //guid we are generating for storing the paymentID received in session

                    //after calling the create function and it is used in the payment execution

                    var guid = Convert.ToString((new Random()).Next(100000));

                    //CreatePayment function gives us the payment approval url

                    //on which payer is redirected for paypal acccount payment

                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    //get links returned from paypal in response to Create function call

                    var links = createdPayment.links.GetEnumerator();

                    string paypalRedirectUrl = null;

                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;

                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // saving the paymentID in the key guid
                    Session.Add(guid, createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This section is executed when we have received all the payments parameters

                    // from the previous call to the function Create

                    // Executing a payment

                    var guid = Request.Params["guid"];

                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);

                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }

                }
            }
            catch (Exception ex)
            {
                // Logger.Log( ex.Message.ToString());
                return View("FailureView");
            }

            return View("SuccessView");
        }

        private PayPal.Api.Payment payment;

        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution() { payer_id = payerId };
            this.payment = new Payment() { id = paymentId };
            return this.payment.Execute(apiContext, paymentExecution);
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            //similar to credit card create itemlist and add item objects to it
            var itemList = new ItemList() { items = new List<Item>() };

            itemList.items.Add(new Item()
            {
                name = "Item Name",
                currency = "USD",
                price = "5",
                quantity = "1",
                sku = "sku"
            });

            var payer = new Payer() { payment_method = "paypal" };

            // Configure Redirect Urls here with RedirectUrls object
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl,
                return_url = redirectUrl
            };

            // similar as we did for credit card, do here and create details object
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "5"
            };

            // similar as we did for credit card, do here and create amount object
            var amount = new Amount()
            {
                currency = "USD",
                total = "7", // Total must be equal to sum of shipping, tax and subtotal.
                details = details
            };

            var transactionList = new List<Transaction>();

            transactionList.Add(new Transaction()
            {
                description = "Transaction description.",
                invoice_number = "your invoice number",
                amount = amount,
                item_list = itemList
            });

            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            // Create a payment using a APIContext
            return this.payment.Create(apiContext);

        }
        private Payment ToPaymentModel(PaymentView model)
        {
            //create and item for which you are taking payment
            //if you need to add more items in the list
            //Then you will need to create multiple item objects or use some loop to instantiate object
            Item item = new Item();
            item.name = model.Name;
            item.currency = model.Currency;
            item.price = model.Price.ToString();
            item.quantity = model.Quantity.ToString();
            item.sku = model.SKU;

            //Now make a List of Item and add the above item to it
            //you can create as many items as you want and add to this list
            List<Item> itms = new List<Item>();
            itms.Add(item);
            ItemList itemList = new ItemList();
            itemList.items = itms;

            //Address for the payment
            Address billingAddress = new Address();
            billingAddress.city = model.BillingAddressCity;
            billingAddress.country_code = "UK";
            billingAddress.line1 = model.BillingAddressLine1;
            billingAddress.postal_code = model.BillingAddressPostCode;
            billingAddress.state = "";


            //Now Create an object of credit card and add above details to it
            CreditCard crdtCard = new CreditCard();
            crdtCard.billing_address = billingAddress;
            crdtCard.cvv2 = model.CVV2;
            crdtCard.expire_month = Convert.ToInt32(model.ExpireMonth);
            crdtCard.expire_year = Convert.ToInt32(model.ExpireYear);
            crdtCard.first_name = model.FirstName;
            crdtCard.last_name = model.LastName;
            crdtCard.number = model.Number;
            crdtCard.type = model.Type;


            // Specify details of your payment amount.
            Details details = new Details();
            details.shipping = "1";
            details.subtotal = model.Price.ToString();
            details.tax = "1";

            // Specify your total payment amount and assign the details object
            Amount amnt = new Amount();
            amnt.currency = "GBP";
            // Total = shipping tax + subtotal.
            amnt.total = model.Total;
            amnt.details = details;

            // Now make a trasaction object and assign the Amount object
            Transaction tran = new Transaction();
            tran.amount = amnt;
            tran.description = model.Description;
            tran.item_list = itemList;
            tran.invoice_number = model.InvoiceNumber;

            // Now, we have to make a list of trasaction and add the trasactions object
            // to this list. You can create one or more object as per your requirements

            List<Transaction> transactions = new List<Transaction>();
            transactions.Add(tran);

            // Now we need to specify the FundingInstrument of the Payer
            // for credit card payments, set the CreditCard which we made above

            FundingInstrument fundInstrument = new FundingInstrument();

            fundInstrument.credit_card = crdtCard;

            // The Payment creation API requires a list of FundingIntrument
            List<FundingInstrument> fundingInstrumentList = new List<FundingInstrument>();
            fundingInstrumentList.Add(fundInstrument);

            // Now create Payer object and assign the fundinginstrument list to the object
            Payer payr = new Payer();
            payr.funding_instruments = fundingInstrumentList;
            payr.payment_method = "credit_card";

            // finally create the payment object and assign the payer object & transaction list to it
            Payment payment = new Payment();
            payment.intent = "sale";
            payment.payer = payr;
            payment.transactions = transactions;

            return payment;
            //crdtCard = new CreditCard()
            //{
            //    billing_address = new Address()
            //    {
            //        city = "Johnstown",
            //        country_code = "US",
            //        line1 = "52 N Main ST",
            //        postal_code = "43210",
            //        state = "OH"
            //    },
            //    cvv2 = "874",
            //    expire_month = 11,
            //    expire_year = 2018,
            //    first_name = "Joe",
            //    last_name = "Shopper",
            //    number = "4024007185826731", //New Credit card Number, Only Card type should match other details does not matter
            //    type = "visa"
            //};

        }

    }
}