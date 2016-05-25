using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class BookingController : Controller
    {
        // GET: Booking
        [HttpGet]
        public ActionResult Index()
        {

            BookingGuestViewModel model = new BookingGuestViewModel();

            return View(model);
        }

        // GET: Booking
        [HttpPost]
        public ActionResult Index(BookingGuestViewModel model)
        {

            if (ModelState.IsValid)
            {
              
            }

            return View();
        }
    }
}