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
        //public ActionResult Index(QuoteModelView quotemodel)
        public ActionResult Index(int airportId, string discount, string dropoffDate , string returnDate , decimal price)       
        {

            BookingGuestViewModel model = new BookingGuestViewModel();

            model.Price = price;
            model.AirportId = airportId;
            model.DropOffDate = Convert.ToDateTime(dropoffDate);
            model.ReturnDate = Convert.ToDateTime(returnDate);
            model.CouponDiscount = discount;
            model.BookingFee = 0;

            return View(model);
        }

        [HttpPost]
        public ActionResult lilo(BookingGuestViewModel model)
        {

            if (ModelState.IsValid)
            {

            }

            return View();
        }



        // GET: Booking
        [HttpPost]
        public ActionResult Index(BookingGuestViewModel model)
        {

            if (ModelState.IsValid)
            {
              
            }

            return View(model);
        }
    }
}