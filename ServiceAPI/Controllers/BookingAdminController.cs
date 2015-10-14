using ACP.Business.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

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


            List<PaymentModel> payments = new List<PaymentModel>
            {
                new PaymentModel
                {
                    Id=1,
                    //Customer = customer,                
                    PaymentMethod =  ACP.Business.Enums.PaymentMethod.CreditCard,
                    CreditCard = new CreditCardModel {
                        Lock = false,
                        ExpiryDate = DateTime.Now.AddYears(2),
                        Deleted = false,
                        Name = "Mike Smith",
                        PlainNumber = "6376485484737833",
                        Type= 2,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatedBy = "localuser",
                        ModifiedBy = "localuser",
                    },
                    Currency = new CurrencyModel {
                          CountryCode = "GB",
                          Code="GPR"
                    },
                    Status =  ACP.Business.Enums.StatusType.Active,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedBy = "localuser",
                    ModifiedBy = "localuser",
                },
                    new PaymentModel
                {
                    Id=2,
                    PaymentMethod =  ACP.Business.Enums.PaymentMethod.CreditCard,
                    CreditCard = new CreditCardModel {
                        Lock = false,
                        ExpiryDate = DateTime.Now.AddYears(2),
                        Deleted = false,
                        Name = "Susana Lajusticia",
                        PlainNumber = "6376485484737833",
                        Type= 2,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        CreatedBy = "localuser",
                        ModifiedBy = "localuser",
                    },
                    Currency = new CurrencyModel {
                          CountryCode = "GB",
                          Code="GPR"
                    },
                    Status =  ACP.Business.Enums.StatusType.Active,
                    Created = DateTime.Now,
                    Modified = DateTime.Now,
                    CreatedBy = "localuser",
                    ModifiedBy = "localuser",
                }
            };

            ViewBag.payments = payments;

            return View();
        }

        // POST: BookingAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection model)
        {
            try
            {
                foreach (var key in model.AllKeys)
                {
                    var value = model[key];
                    // etc.
                }
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
