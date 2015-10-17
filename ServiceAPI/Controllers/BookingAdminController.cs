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
        
        public BookingAdminController(BookingController bookingcontroller)
        {
            _bookingcontroller = bookingcontroller;
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
        public ActionResult Create()
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
                    
                    var paymentform = Request.Form;

                    //## Bank account
                    if (paymentform["paymenttype"] == "1")
                    {
                        BankAccountModel bankaccount = new BankAccountModel();
                        bankaccount.AccountName = paymentform["baname"];
                        bankaccount.BankName = paymentform["babankname"];
                        bankaccount.AbaRouting = paymentform["basortcode"];
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
                        payment.CreditCard = creditcard;
                    }
                    model.Payments.Add(payment);
                        

                    _bookingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _bookingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _bookingcontroller.Add(model);

                    result.TryGetContentValue(out booking);

                    if (booking != null)
                        return RedirectToAction("Index");
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
