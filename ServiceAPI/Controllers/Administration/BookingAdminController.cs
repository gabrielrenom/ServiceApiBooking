using ACP.Business.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Collections;
using System.Collections.ObjectModel;
using ServiceAPI.Models;
using ACP.Business.Services.Interfaces;
using System.Configuration;
using ACP.Business.Enums;

namespace ServiceAPI.Controllers
{
    public class BookingAdminController : Controller
    {
        private BookingController _bookingcontroller;
        private CarParkController _carparkcontroller;
        private AirportController _airportcontroller;
        private QuoteController _quotecontroller;
        private IPayPalService _paypalservice;



        public BookingAdminController(BookingController bookingcontroller, AirportController airportcontroller, CarParkController carparkcontroller, QuoteController quotecontroller, IPayPalService paypalservice)
        {
            _bookingcontroller = bookingcontroller;
            _carparkcontroller = carparkcontroller;
            _airportcontroller = airportcontroller;
            _quotecontroller = quotecontroller;
            _paypalservice = paypalservice;
        }

        private BookingModel ToBookingModel(BookingModel model)
        {
            //## The client books but it is flaged to false,
            //## Only when he pays it is flagged to true
            BookingModel bookingModel = new BookingModel();

            bookingModel.User = new UserModel
            {
                Created = DateTime.Now,
                CreatedBy = model.Customer.Email,
                Modified = DateTime.Now,
                ModifiedBy = model.Customer.Email,
                Email = model.Customer.Email,
                FirstName = model.Customer.Forename,
                LastName = model.Customer.Surname,
                Gender = model.Customer.Title.ToLower().Contains('s') ? ACP.Business.Enums.Gender.Female : ACP.Business.Enums.Gender.Male,
                DOB = DateTime.Now,

            };


            //if (model.CreditCardType != null)
            //{
            //    CreditCardTypes creditcardtype = CreditCardTypes.Visa;
            //    if (model.CreditCardType.ToLower() == "visa") creditcardtype = CreditCardTypes.Visa;
            //    else if (model.CreditCardType.ToLower() == "maestro") creditcardtype = CreditCardTypes.Maestro;
            //    else if (model.CreditCardType.ToLower() == "mastercard") creditcardtype = CreditCardTypes.Mastercard;
            //    else if (model.CreditCardType.ToLower() == "americanexpress") creditcardtype = CreditCardTypes.AmericanExpress;

            //    bookingModel.Payments = model.CreditCardType != null ? new Collection<PaymentModel>
            //    {
            //        new PaymentModel{ CreditCard = new CreditCardModel{ ExpiryDate = new DateTime(Convert.ToInt32(model.ExpiryYear), Convert.ToInt32(model.ExpiryMonth), 1),
            //            Created = DateTime.Now,
            //            CreatedBy = "System",
            //            Deleted = false,
            //            Name = model.CardName,
            //            Modified =DateTime.Now,
            //            ModifiedBy = "System",
            //            Number = model.CardNumber,
            //            Type =creditcardtype,
            //            PlainNumber = model.CardNumber,

            //        },
            //         CreatedBy = "System",
            //         Status = StatusType.Paid,
            //         Modified = DateTime.Now,
            //         ModifiedBy = "System",
            //         Created = DateTime.Now,
            //         CurrencyId = 1,
            //         PaymentMethod = PaymentMethod.CreditCard
            //    }
            //} : null;
            //}
            bookingModel.Car = new CarModel
            {
                Created = DateTime.Now,
                CreatedBy = model.Customer.Email,
                Modified = DateTime.Now,
                ModifiedBy = model.Customer.Email,
                Model = model.Car.Model,
                Make = model.Car.Make,
                Registration = model.Car.Registration,
                Colour = model.Car.Colour,
                User = new UserModel
                {
                    Address = new AddressModel
                    {
                        Created = DateTime.Now,
                        CreatedBy = model.Customer.Email,
                        Modified = DateTime.Now,
                        ModifiedBy = model.Customer.Email,
                    },
                    Created = DateTime.Now,
                    CreatedBy = model.Customer.Email,
                    Modified = DateTime.Now,
                    ModifiedBy = model.Customer.Email,
                    Email = model.Customer.Email,
                    FirstName = model.Customer.Forename,
                    LastName = model.Customer.Surname,
                    Gender = model.Customer.Title.ToLower().Contains('s') ? ACP.Business.Enums.Gender.Female : ACP.Business.Enums.Gender.Male
                }
            };

            bookingModel.TravelDetails = new TravelDetailsModel
            {
                Created = DateTime.Now,
                CreatedBy = model.Customer.Email,
                Modified = DateTime.Now,
                ModifiedBy = model.Customer.Email,
                OutboundDate = model.TravelDetails.OutboundDate,
                ReturnDate = model.TravelDetails.ReturnDate,
                OutboundFlight = model.TravelDetails.OutboundFlight,
                OutboundTerminal = model.TravelDetails.OutboundTerminal,
                ReturnboundTerminal = model.TravelDetails.ReturnboundTerminal,
                ReturnFlight = model.TravelDetails.ReturnFlight,
            };

            bookingModel.Cost = Convert.ToDouble(model.Price);
            bookingModel.StartDate = model.TravelDetails.OutboundDate;
            bookingModel.EndDate = model.TravelDetails.ReturnDate;
            bookingModel.Status = ACP.Business.Enums.StatusType.Processing;
            bookingModel.Modified = DateTime.Now;
            bookingModel.Extras = new List<ExtraModel>
            {
                //new ExtraModel {
                //    Created = DateTime.Now,
                //    CreatedBy = model.Customer.Email,
                //    Modified = DateTime.Now,
                //    ModifiedBy = model.Customer.Email,
                //    BookingEntityId = model.Extras.ookEntityId,
                //    Name = "Parking",
                //    Price = model.Price
                //}
            };

            bookingModel.Customer = new CustomerModel
            {
                Email = model.Customer.Email,
                Initials = model.Customer.Title,
                Forename = model.Customer.Forename,
                Surname = model.Customer.Surname,
                Mobile = model.Customer.Mobile,
                Created = DateTime.Now,
                Modified = DateTime.Now,
                Address = new AddressModel
                {
                    Created = DateTime.Now,
                    CreatedBy = model.Customer.Email,
                    Modified = DateTime.Now,
                    ModifiedBy = model.Customer.Email,
                    Address1 = model.Customer.Address.Address1,
                    City = model.Customer.Address.Address1,
                    Postcode = model.Customer.Address.Postcode
                }
            };

            bookingModel.Status = ACP.Business.Enums.StatusType.Processing;
            return bookingModel;
        }


        // GET: BookingAdmin
        public async Task<ActionResult> Index()
        {
            List<BookingModel> bookings = new List<BookingModel>();
            _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var result = await _bookingcontroller.GetAll();

            result.TryGetContentValue<List<BookingModel>>(out bookings);

            return View(bookings);
        }

        // GET: BookingAdmin/Details/5
        public async  Task<ActionResult> Details(int id)
        {
            BookingModel booking = new BookingModel();
            if (ModelState.IsValid)
            {
                _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _bookingcontroller.GettById(id);

                result.TryGetContentValue(out booking);

                if (booking != null)
                {

                }
                return View(booking);
            }

            return View();
        }

        // GET: BookingAdmin/Create
        public async Task<ActionResult> Create()
        {
            BookingModel model = new BookingModel();
          
            _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var result = await _bookingcontroller.GetModel();

            result.TryGetContentValue(out model);

            if (result != null)
            {
                model.Customer = new CustomerModel();
                model.Customer.Address = new AddressModel();
                model.TravelDetails = new TravelDetailsModel();
                await FillDropBoxes();
                return View(model);
            }


            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult> Paid(int id)
        {
            var procesingpayment = (await _bookingcontroller.Paid(id));

            return View();
        }

        // POST: BookingAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(BookingModel model)
        {
            try
            {
                BookingModel booking = new BookingModel();
                if (ModelState.IsValid)
                {
                    model.Payments = new List<PaymentModel>();
                    PaymentModel payment = new PaymentModel();
                    payment.Created = DateTime.Now;
                    payment.Modified = DateTime.Now;
                    payment.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    payment.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                   
                    var paymentform = Request.Form;

                    payment.CurrencyId = Convert.ToInt32(paymentform["currency"]);

                    //## Bank account
                    if (paymentform["paymenttype"] != "100")
                    {
                        BankAccountModel bankaccount = new BankAccountModel();
                        bankaccount.AccountName = paymentform["baname"];
                        bankaccount.BankName = paymentform["babankname"];
                        bankaccount.AbaRouting = paymentform["basortcode"];
                        bankaccount.Created = DateTime.Now;
                        bankaccount.Modified = DateTime.Now;
                        bankaccount.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                        bankaccount.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                        payment.PaymentMethod = PaymentMethod.CreditCard;
                        payment.BankAccount = bankaccount;
                    }
                    else
                    {
                        //##Credit card
                        CreditCardModel creditcard = new CreditCardModel();
                        creditcard.Type = (ACP.Business.Enums.CreditCardTypes) Convert.ToInt32(paymentform["ccardtype"]);
                        creditcard.Name = paymentform["ccname"];
                        creditcard.Number = paymentform["ccnumber"];
                        creditcard.ExpiryDate = new DateTime(Convert.ToInt32(paymentform["year"]), Convert.ToInt32(paymentform["month"]), 1); 
                        creditcard.GateWayKey = paymentform["cccsv"];
                        creditcard.Created = DateTime.Now;
                        creditcard.Modified = DateTime.Now;
                        creditcard.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                        creditcard.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                        payment.PaymentMethod=PaymentMethod.CreditCard;
                        payment.CreditCard = creditcard;
                        creditcard.Type = (CreditCardTypes)Convert.ToInt32(paymentform["ccardtype"]);
                    }
                    model.Payments.Add(payment);

                    model.User= new UserModel
                    {
                        FirstName = model.Customer.Forename,                        
                        LastName = model.Customer.Surname,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatedBy = model.Customer.Forename + " " + model.Customer.Surname,
                        ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname,
                        DOB = DateTime.Now
                    };

                    model.Created = DateTime.Now;
                    model.Modified = DateTime.Now;
                    model.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    model.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                    model.TravelDetails.Created = DateTime.Now;
                    model.TravelDetails.Modified = DateTime.Now;
                    model.TravelDetails.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    model.TravelDetails.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                    model.Car.Created = DateTime.Now;
                    model.Car.Modified = DateTime.Now;
                    model.Car.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    model.Car.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    model.Car.User = new UserModel
                    {
                        FirstName = model.Customer.Forename,
                        LastName = model.Customer.Surname,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatedBy = model.Customer.Forename + " " + model.Customer.Surname,
                        ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname,
                        DOB= DateTime.Now                      
                    };

                    model.Customer.Created = DateTime.Now;
                    model.Customer.Modified = DateTime.Now;
                    model.Customer.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    model.Customer.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                    model.Extras = new List<ExtraModel>
                    {
                        new ExtraModel
                        {
                            BookingEntityId = Convert.ToInt32(paymentform["carpark"]),
                            Name="Parking",
                            Price =model.Price
                        }
                    };
                    model.Cost = Convert.ToDouble(model.Price);

                    _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                   // model.SourceCode = paymentform["airport"];

                    var result = await _bookingcontroller.Add(model);

                    result.TryGetContentValue(out booking);

                    
                    //var paymentresult = _paypalservice.PaymentWithCreditCard(ToPayPalModel(booking,model), Models.Configuration.GetAPIContext());
                    //var paymentresult = _paypalservice.PaymentWithCreditCard(ToPayPalModel(booking, model), null);

                    //if (paymentresult == "approved")
                    //{
                        var procesingpayment = (await _bookingcontroller.PaymentInProgress(booking.Id));
                        //if (havebeenpaid) return RedirectToAction("paymentcompleted", ToBookingConfirmationView(model, result.BookingReference));
                    //}

                    result.TryGetContentValue(out booking);

                    if (booking != null && procesingpayment)
                    {
                        await FillDropBoxes();
                        model.Status = StatusType.Processing;
                        return View(model);
                    }
                        //return RedirectToAction("Index");
                }
                else
                {
                    await FillDropBoxes();
                            
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                await FillDropBoxes();
                
                return View("Create");
            }

            return View("Create");
        }

        private PayPalModel ToPayPalModel(BookingModel booking, BookingModel model)
        {
            var paypal = new PayPalModel
            {
                FirstName = model.Customer.Forename,
                LastName = model.Customer.Surname,
                Currency = "GBP",
                Description = model.Message,
                Name = booking.Customer.Forename + " " + booking.Customer.Surname,
                Total = Convert.ToString(model.Price),
                Number = model.Payments.FirstOrDefault().CreditCard.Number,
                BillingAddressCity = model.Customer.Address.City,
                BillingAddressCountry = "UK",
                BillingAddressLine1 = model.Customer.Address.Address1,
                BillingAddressPostCode = model.Customer.Address.Postcode,
                BillingAddressState = "",
                Price = model.Price.ToString(),
                Quantity = 1,
                ExpireYear = model.Payments.FirstOrDefault().CreditCard.ExpiryDate.Year.ToString(),
                ExpireMonth = Convert.ToInt32(model.Payments.FirstOrDefault().CreditCard.ExpiryDate.Month).ToString(),
                CVV2 = model.Payments.FirstOrDefault().CreditCard.GateWayKey,
                SKU = "123",
                InvoiceNumber = new Random().Next(1000, 10000).ToString()
            };
            if (model.Payments.FirstOrDefault().CreditCard.Type.ToString() == "0")
                paypal.Type = "visa";
            else if (model.Payments.FirstOrDefault().CreditCard.Type.ToString() == "1")
                paypal.Type = "mastercard";
            else if (model.Payments.FirstOrDefault().CreditCard.Type.ToString() == "2")
                paypal.Type = "americanexpress";

            return paypal;
        }

        private PayPalModel ToPayPalModel(BookingGuestViewModel model)
        {
            var paypal = new PayPalModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Currency = "GBP",
                Description = model.Description,
                Name = model.CardName,
                Total = Convert.ToString(model.Price + model.BookingFee),
                Number = model.CardNumber,
                BillingAddressCity = model.City,
                BillingAddressCountry = "UK",
                BillingAddressLine1 = model.Address,
                BillingAddressPostCode = model.Postcode,
                BillingAddressState = "",
                Price = model.Price.ToString(),
                Quantity = 1,
                ExpireYear = model.ExpiryYear,
                ExpireMonth = Convert.ToInt32(model.ExpiryMonth).ToString(),
                CVV2 = model.CVV,
                SKU = "123",
                InvoiceNumber = new Random().Next(1000, 10000).ToString()
            };
            if (model.CreditCardType == "0")
                paypal.Type = "visa";
            else if (model.CreditCardType == "1")
                paypal.Type = "mastercard";
            else if (model.CreditCardType == "2")
                paypal.Type = "americanexpress";

            return paypal;
        }

        // GET: BookingAdmin/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            BookingModel booking = new BookingModel();
            if (ModelState.IsValid)
            {
                _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _bookingcontroller.GettById(id);

                result.TryGetContentValue(out booking);
                await FillDropBoxes();

                if (booking != null)
                {
                    
                }
                    return View(booking);
            }
            else
                await FillDropBoxes();


            return View();
        }

        // POST: BookingAdmin/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(BookingModel model)
        {
            bool updated = false;
            BookingModel booking = new BookingModel();
            try
            {
                if (model.EndDate < model.StartDate)
                {
                    ModelState.AddModelError(string.Empty, "The End date has to older than the start date");

                    _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _bookingcontroller.GettById(model.Id);

                    result.TryGetContentValue(out booking);
                    booking.EndDate = model.EndDate;
                    booking.StartDate = model.StartDate;
                    await FillDropBoxes();

                    return View(booking);
                }

               
                if (ModelState.IsValid)
                {
                    _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var bookingresult = await _bookingcontroller.GettById(model.Id);

                    bookingresult.TryGetContentValue(out booking);

                    if (booking.Payments.Count == 0)
                    {
                        booking.Payments = new Collection<PaymentModel> { new PaymentModel()};
                    }

                    booking.Payments.FirstOrDefault().Modified = DateTime.Now;
                    booking.Payments.FirstOrDefault().ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                    var paymentform = Request.Form;

                    booking.Payments.FirstOrDefault().CurrencyId = Convert.ToInt32(paymentform["currency"]);

                    //## Bank account
                    if (paymentform["paymenttype"] != "100")
                    {
                        if (booking.Payments.FirstOrDefault().BankAccount == null)
                            booking.Payments.FirstOrDefault().BankAccount = new BankAccountModel();
                        booking.Payments.FirstOrDefault().BankAccount.AccountName = paymentform["baname"];
                        booking.Payments.FirstOrDefault().BankAccount.BankName = paymentform["babankname"];
                        booking.Payments.FirstOrDefault().BankAccount.AbaRouting = paymentform["basortcode"];
                        booking.Payments.FirstOrDefault().BankAccount.Modified = DateTime.Now;
                        booking.Payments.FirstOrDefault().BankAccount.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                    }
                    else
                    {
                        //##Credit card
                        if (booking.Payments.FirstOrDefault().CreditCard== null)
                            booking.Payments.FirstOrDefault().CreditCard=new CreditCardModel();
                        booking.Payments.FirstOrDefault().CreditCard.Type = (ACP.Business.Enums.CreditCardTypes)Convert.ToInt32(paymentform["ccardtype"]);
                        booking.Payments.FirstOrDefault().CreditCard.Name = paymentform["ccname"];
                        booking.Payments.FirstOrDefault().CreditCard.Number = paymentform["ccnumber"];
                        booking.Payments.FirstOrDefault().CreditCard.ExpiryDate = Convert.ToDateTime(paymentform["ccexpirydate"]);
                        booking.Payments.FirstOrDefault().CreditCard.GateWayKey = paymentform["cccsv"];
                        booking.Payments.FirstOrDefault().CreditCard.Modified = DateTime.Now;
                        booking.Payments.FirstOrDefault().CreditCard.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                        await FillDropBoxes();
                    }

                    booking.Modified = DateTime.Now;
                    booking.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    booking.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    booking.Message = model.Message;                    

                    booking.TravelDetails.Modified = DateTime.Now;                    
                    booking.TravelDetails.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    booking.TravelDetails.OutboundDate = model.TravelDetails.OutboundDate;
                    booking.TravelDetails.OutboundFlight = model.TravelDetails.OutboundFlight;
                    booking.TravelDetails.OutboundTerminal = model.TravelDetails.OutboundTerminal;
                    booking.TravelDetails.ReturnFlight = model.TravelDetails.ReturnFlight;
                    booking.TravelDetails.ReturnDate = model.TravelDetails.ReturnDate;
                    booking.TravelDetails.ReturnboundTerminal = model.TravelDetails.ReturnboundTerminal;

                    booking.Car.Created = DateTime.Now;
                    booking.Car.Modified = DateTime.Now;
                    booking.Car.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    booking.Car.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    booking.Car.Colour = model.Car.Colour;
                    booking.Car.Make = model.Car.Make;
                    booking.Car.Model = model.Car.Model;
                    booking.Car.Registration = model.Car.Registration;

                    booking.Customer.Modified = DateTime.Now;
                    booking.Customer.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    booking.Customer.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    booking.Customer.Fax = model.Customer.Fax;
                    booking.Customer.Forename = model.Customer.Forename;
                    booking.Customer.Initials = model.Customer.Initials;
                    booking.Customer.Mobile = model.Customer.Mobile;
                    booking.Customer.Surname = model.Customer.Surname;
                    booking.Customer.Telephone = model.Customer.Telephone;
                    booking.Customer.Title = model.Customer.Title;
                    booking.StartDate = model.StartDate;
                    booking.EndDate = model.EndDate;

                    booking.BookingReference = model.BookingReference;
                    booking.Created = model.Created;
                    booking.Modified = DateTime.Now;
                    booking.CreatedBy = "System";
                    booking.ModifiedBy = "System";
                    booking.Cost = model.Cost;



                    _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    //model.SourceCode = paymentform["airport"];
                    foreach (var item in booking.Extras)
                    {
                        item.BookingEntityId = Convert.ToInt32(paymentform["airport"]);
                    }
                   
                    var result = await _bookingcontroller.Update(booking);

                    result.TryGetContentValue(out updated);

                    if (updated == true)
                        return RedirectToAction("Index");
                }
                else
                {
                    await FillDropBoxes();

                    return View(model);
                }
            }
            catch (Exception ex)
            {
                await FillDropBoxes();

                return View("Edit");
            }

            return View("Edit");
        }

        // GET: BookingAdmin/Delete/5
        public async Task<ActionResult> Delete(int id)
        {

            BookingModel booking = new BookingModel();
            if (ModelState.IsValid)
            {
                _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _bookingcontroller.GettById(id);

                result.TryGetContentValue(out booking);
                
                return View(booking);
            }

            return View();            
        }

        // POST: BookingAdmin/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            try
            {
                BookingModel booking = new BookingModel();
                if (ModelState.IsValid)
                {
                    _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _bookingcontroller.Delete(id);

                    result.TryGetContentValue(out booking);                    
                }


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private async Task FillDropBoxes(int? creditcard = null)
        {
            var cctypelist = new List<SelectListItem>();
            cctypelist.Add(new SelectListItem() { Text = "Visa", Value = "100", Selected= creditcard==1?true:false });
            cctypelist.Add(new SelectListItem() { Text = "Mastercard", Value = "101", Selected = creditcard == 2 ? true : false });
            cctypelist.Add(new SelectListItem() { Text = "American Express", Value = "102", Selected = creditcard == 3 ? true : false });
            cctypelist.Add(new SelectListItem() { Text = "Maestro", Value = "103", Selected = creditcard == 4 ? true : false });
            ViewBag.cctype = cctypelist;

            var paymenttypelist = new List<SelectListItem>();
            //paymenttypelist.Add(new SelectListItem() { Text = "Bank Account", Value = "101",Selected = creditcard != null ? true : false });
            paymenttypelist.Add(new SelectListItem() { Text = "Credit Card", Value = "100", Selected = creditcard !=null ? true : false });
            ViewBag.paymenttype = paymenttypelist;

            var currency = new List<SelectListItem>();
            currency.Add(new SelectListItem() { Text = "£", Value = "1" });
            ViewBag.currency = currency;

            var statuses = new List<SelectListItem> {
                new SelectListItem { Text = ACP.Business.Enums.StatusType.Processing.ToString(), Value = ((int)ACP.Business.Enums.StatusType.Processing).ToString() },
                new SelectListItem { Text = ACP.Business.Enums.StatusType.Active.ToString(), Value = ((int)ACP.Business.Enums.StatusType.Active).ToString() },
                new SelectListItem { Text = ACP.Business.Enums.StatusType.Inactive.ToString(), Value = ((int)ACP.Business.Enums.StatusType.Inactive).ToString() },
                new SelectListItem { Text = ACP.Business.Enums.StatusType.Completed.ToString(), Value = ((int)ACP.Business.Enums.StatusType.Completed).ToString() },
                new SelectListItem { Text = ACP.Business.Enums.StatusType.Paid.ToString(), Value = ((int)ACP.Business.Enums.StatusType.Paid).ToString() }
            };
            ViewBag.statuses = statuses;

            var carparkslist = new List<SelectListItem>();
            List<BookingEntityModel> carparks = new List<BookingEntityModel>();
            _carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var result = await _carparkcontroller.GetAll();
            result.TryGetContentValue(out carparks);
            if (carparks != null)
            {
                foreach (var carpark in carparks)
                {
                    carparkslist.Add(new SelectListItem() { Text = carpark.Name, Value = carpark.Id.ToString(), Group = new SelectListGroup() { Name = carpark.RootBookingEntity.Name } });
                }
            }
            //ViewBag.carparkslist = carparkslist;
            ViewBag.carparkslist = carparks;

            var airportlist = new List<SelectListItem>();
            List<RootBookingEntityModel> airports = new List<RootBookingEntityModel>();
            _airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _airportcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var airportsresult = await _airportcontroller.GetAll();
            airportsresult.TryGetContentValue(out airports);
            if (airports != null)
            {
                foreach (var airport in airports)
                {
                    airportlist.Add(new SelectListItem() { Text = airport.Name, Value = airport.Code });
                }
            }
            ViewBag.airportlist = airportlist;
            
        }

        [HttpGet]
        [Route("results")]
        public async Task<ActionResult> Results(QuoteModelView model)
        {
            ViewBag.Searched = model;
            //if (ModelState.IsValid)
            //{
                try
                {
                    if (model != null)
                    {
                        QuoteModel quotemodel = new QuoteModel();
                        _quotecontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                        _quotecontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                        var result = await _quotecontroller.GetQuoteWithPriceByCarcarkIdAndDates(model.Id, Convert.ToDateTime(model.ReturnDate), Convert.ToDateTime(model.DropOffDate));

                        result.TryGetContentValue<QuoteModel>(out quotemodel);

                        return Json(quotemodel, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            //}
            return Json(null);
        }

        private List<ResultsView> ToResultsView(QuoteModel model, QuoteModelView quote)
        {
            List<ResultsView> domainModel = new List<ResultsView>();

            domainModel = model.Pricing != null ? model.Pricing.Select(x => new ResultsView
            {
                Address = x.PriceModel.BookingEntity.Address != null ? new AddressView
                {
                    Address1 = x.PriceModel.BookingEntity.Address.Address1,
                    Address2 = x.PriceModel.BookingEntity.Address.Address2,
                    City = x.PriceModel.BookingEntity.Address.City,
                    Country = x.PriceModel.BookingEntity.Address.Country,
                    County = x.PriceModel.BookingEntity.Address.County,
                    Number = x.PriceModel.BookingEntity.Address.Number,
                    Postcode = x.PriceModel.BookingEntity.Address.Postcode
                } : new AddressView(),
                Price = x.Price,
                Company = x.PriceModel.BookingEntity.Name,
                CompanyLogo = x.PriceModel.BookingEntity.Image == null ? new byte[] { } : x.PriceModel.BookingEntity.Image,
                Description = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "description").FirstOrDefault() != null ? x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "description").FirstOrDefault().Value : "",
                DistanceFromAirport = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "airportdistance").FirstOrDefault() != null ? (decimal?)Convert.ToDecimal(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "airportdistance").FirstOrDefault().Value) : null,
                TransferTime = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "transfer").FirstOrDefault() != null ? (decimal?)Convert.ToDecimal(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "transfer").FirstOrDefault().Value) : 0,
                IsRegularTransfers = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isregulartransfers").FirstOrDefault() != null ? (bool)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isregulartransfers").FirstOrDefault().Value) : false,
                IsFamilyFriendly = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isfamilyfriendly").FirstOrDefault() != null ? (bool)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isfamilyfriendly").FirstOrDefault().Value) : false,
                IsRetainKeys = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isretainkeys").FirstOrDefault() != null ? (bool)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isretainkeys").FirstOrDefault().Value) : false,
                Is24hSecurity = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isretainkeys").FirstOrDefault() != null ? (bool)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "is24security").FirstOrDefault().Value) : false,
                IsParkAndRide = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "parkandride").FirstOrDefault() != null ? (bool)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "parkandride").FirstOrDefault().Value) : false,
                IsMeetAndGreet = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "meetandgreet").FirstOrDefault() != null ? (bool)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "meetandgreet").FirstOrDefault().Value) : false,
                IsOnAirport = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "onairport").FirstOrDefault() != null ? (bool)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "onairport").FirstOrDefault().Value) : false,
                Summary = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "summary").FirstOrDefault() != null ? x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "summary").FirstOrDefault().Value : "",
                Id = x.PriceModel.BookingEntity.Id,
                Important = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "important").FirstOrDefault() != null ? x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "important").FirstOrDefault().Value : "",
                FullString = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "fullstring").FirstOrDefault() != null ? x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "fullstring").FirstOrDefault().Value : "",
                //Reviews = x.PriceModel.BookingEntity.Reviews != null ? x.PriceModel.BookingEntity.Reviews.Select(u => new ReviewView
                //{
                //    ClientName = u.Author,
                //    Subject = u.Subject,
                //    PublicationDate = (DateTime)u.Created,
                //    Review = u.Comments,
                //    Rating = u.Rating
                //}).ToList() : null,
                Quote = quote
            }).ToList() : null;

            return domainModel;
        }

    }
}
