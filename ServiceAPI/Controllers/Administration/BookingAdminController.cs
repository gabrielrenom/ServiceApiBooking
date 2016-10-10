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

namespace ServiceAPI.Controllers
{
    public class BookingAdminController : Controller
    {
        BookingController _bookingcontroller;
        CarParkController _carparkcontroller;
        AirportController _airportcontroller;


        public BookingAdminController(BookingController bookingcontroller, AirportController airportcontroller, CarParkController carparkcontroller)
        {
            _bookingcontroller = bookingcontroller;
            _carparkcontroller = carparkcontroller;
            _airportcontroller = airportcontroller;

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
            await FillDropBoxes();

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
                    //CurrencyModel currency = new CurrencyModel {
                    //     Code="GBP",
                    //     Symbol="£",
                    //     CountryCode="GB",
                    //     Created = DateTime.Now,
                    //     Modified = DateTime.Now,
                    //     CreatedBy = model.Customer.Forename + " " + model.Customer.Surname,
                    //     ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname                         
                    //    };
                    model.Payments = new List<PaymentModel>();
                    PaymentModel payment = new PaymentModel();
                    payment.Created = DateTime.Now;
                    payment.Modified = DateTime.Now;
                    payment.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    payment.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                   
                    var paymentform = Request.Form;

                    payment.CurrencyId = Convert.ToInt32(paymentform["currency"]);

                    //## Bank account
                    if (paymentform["paymenttype"] == "1")
                    {
                        BankAccountModel bankaccount = new BankAccountModel();
                        bankaccount.AccountName = paymentform["baname"];
                        bankaccount.BankName = paymentform["babankname"];
                        bankaccount.AbaRouting = paymentform["basortcode"];
                        bankaccount.Created = DateTime.Now;
                        bankaccount.Modified = DateTime.Now;
                        bankaccount.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                        bankaccount.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                        payment.BankAccount = bankaccount;
                    }
                    else
                    {
                        //##Credit card
                        CreditCardModel creditcard = new CreditCardModel();
                        creditcard.Type = (ACP.Business.Enums.CreditCardTypes) Convert.ToInt32(paymentform["ccardtype"]);
                        creditcard.Name = paymentform["ccname"];
                        creditcard.Number = paymentform["ccnumber"];
                        creditcard.ExpiryDate = Convert.ToDateTime(paymentform["ccexpirydate"]);
                        creditcard.GateWayKey = paymentform["cccsv"];
                        creditcard.Created = DateTime.Now;
                        creditcard.Modified = DateTime.Now;
                        creditcard.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                        creditcard.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                        payment.CreditCard = creditcard;
                        
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
                        DOB= model.User.DOB                       
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

                    model.Customer.Created = DateTime.Now;
                    model.Customer.Modified = DateTime.Now;
                    model.Customer.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    model.Customer.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                    _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    model.SourceCode = paymentform["airport"];
                    var result = await _bookingcontroller.Add(model);

                    result.TryGetContentValue(out booking);

                    if (booking != null)
                        return RedirectToAction("Index");
                }
                else
                {
                    await FillDropBoxes();
                            
                    return View();
                }
            }
            catch
            {
                await FillDropBoxes();

                return View("Create");
            }

            return View("Create");
        }

        // GET: BookingAdmin/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            await FillDropBoxes();

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

        // POST: BookingAdmin/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(BookingModel model)
        {
            bool updated = false;
            try
            {                
                BookingModel booking = new BookingModel();
                if (ModelState.IsValid)
                {
                    _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var bookingresult = await _bookingcontroller.GettById(model.Id);

                    bookingresult.TryGetContentValue(out booking);
                                       
                    booking.Payments.FirstOrDefault().Modified = DateTime.Now;
                    booking.Payments.FirstOrDefault().ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;

                    var paymentform = Request.Form;

                    booking.Payments.FirstOrDefault().CurrencyId = Convert.ToInt32(paymentform["currency"]);

                    //## Bank account
                    if (paymentform["paymenttype"] == "1")
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


                    booking.Car.Created = DateTime.Now;
                    booking.Car.Modified = DateTime.Now;
                    booking.Car.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    booking.Car.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    booking.Car.Colour = model.Car.Colour;
                    booking.Car.Make = model.Car.Make;
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

                    _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    model.SourceCode = paymentform["airport"];
                    var result = await _bookingcontroller.Update(model);

                    result.TryGetContentValue(out updated);

                    if (updated == true)
                        return RedirectToAction("Index");
                }
                else
                {
                    await FillDropBoxes();

                    return View();
                }
            }
            catch
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

        private async Task FillDropBoxes()
        {
            var cctypelist = new List<SelectListItem>();
            cctypelist.Add(new SelectListItem() { Text = "Visa", Value = "1" });
            cctypelist.Add(new SelectListItem() { Text = "Mastercard", Value = "2" });
            cctypelist.Add(new SelectListItem() { Text = "American Express", Value = "3" });
            cctypelist.Add(new SelectListItem() { Text = "Maestro", Value = "3" });
            ViewBag.cctype = cctypelist;

            var paymenttypelist = new List<SelectListItem>();
            paymenttypelist.Add(new SelectListItem() { Text = "Bank Account", Value = "1" });
            paymenttypelist.Add(new SelectListItem() { Text = "Credit Card", Value = "2" });
            ViewBag.paymenttype = paymenttypelist;

            var currency = new List<SelectListItem>();
            currency.Add(new SelectListItem() { Text = "£", Value = "1" });
            ViewBag.currency = currency;

            //var carparkslist = new List<SelectListItem>();
            //List<BookingEntityModel> carparks = new List<BookingEntityModel>();
            //_carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            //_carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            //var result = await _carparkcontroller.GetAll();
            //result.TryGetContentValue(out carparks);
            //if (carparks != null)
            //{
            //    foreach (var carpark in carparks)
            //    {
            //        carparkslist.Add(new SelectListItem() { Text = carpark.Name, Value = carpark.Id.ToString() });
            //    }
            //}
            //ViewBag.carparkslist = carparkslist;

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
    }
}
