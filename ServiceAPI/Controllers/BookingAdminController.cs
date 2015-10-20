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
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BookingAdmin/Create
        public async Task<ActionResult> Create()
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
                    CurrencyModel currency = new CurrencyModel {
                         Code="GBP",
                         Symbol="£",
                         CountryCode="GB",
                         Created = DateTime.Now,
                         Modified = DateTime.Now,
                         CreatedBy = model.Customer.Forename + " " + model.Customer.Surname,
                         ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname                         
                        };
                    model.Payments = new List<PaymentModel>();
                    PaymentModel payment = new PaymentModel();
                    payment.Created = DateTime.Now;
                    payment.Modified = DateTime.Now;
                    payment.CreatedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    payment.ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname;
                    payment.Currency = currency;

                    var paymentform = Request.Form;

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
                        creditcard.ExpiryDate = DateTime.Now;//Convert.ToDateTime(paymentform["ccexpirydate"]);
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
                        ModifiedBy = model.Customer.Forename + " " + model.Customer.Surname
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

                    return View();
                }
            }
            catch
            {
                return View("Create");
            }

            return View("Create");
        }

        // GET: BookingAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BookingAdmin/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BookingAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BookingAdmin/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
