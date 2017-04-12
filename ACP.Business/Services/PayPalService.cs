using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services
{
    public class PayPalService: IPayPalService
    {
        private PayPal.Api.Payment payment;

        public string PaymentWithCreditCard(PayPalModel model, APIContext context)
        {
            try
            {
#if DEBUG
                return "approved";
#else
                APIContext apiContext = context;
                Payment createdPayment = ToPaymentModel(model).Create(apiContext);

                if (createdPayment.state.ToLower() != "approved")
                {
                    return "approved";
                }
#endif
            }
            catch (PayPal.PayPalException ex)
            {
                return ex.ToString();
            }

            return "approved";
        }
        
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

        private Payment ToPaymentModel(PayPalModel model)
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
            details.shipping = "0";
            details.subtotal = model.Price.ToString();
            details.tax = "0";

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
