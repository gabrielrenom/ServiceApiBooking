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
            return View();
        }

        // POST: BookingAdmin/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
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
