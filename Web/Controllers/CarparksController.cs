
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Models;


namespace Web.Controllers
{
    [RoutePrefix("")]
    public class CarparksController : Controller
    {
        private IRootBookingEntityService _airportservice;
        private IQuoteService _quoteservice;

        public CarparksController(
            IRootBookingEntityService airportservice,
            IQuoteService quoteservice)
        {
            _airportservice = airportservice;
            _quoteservice = quoteservice;
        }
        
        [HttpGet]
        [Route("")]
        public async Task<ActionResult> Index()
        {            
            try
            {
                //## IT Gets all the airports
                var airports = await _airportservice.GetAll();

                ViewBag.airports = airports != null ? airports.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList() : null;                
            }
            catch (Exception ex)
            {
               
            }
            return View(new QuoteModelView { Airport="", Discount="", ReturnDate="", Pickup= "" });            
        }

        [HttpGet]
        [Route("results")]
        public async Task<ActionResult> Results(QuoteModelView model)
        {
            QuoteModel quote = new QuoteModel();
            quote.Pickup = new DateTime(2016, 12, 23, 16, 30, 00);
            quote.Dropoff = new DateTime(2016, 12, 28, 18, 40, 00);
            quote.PickupLocation = new LocationModel() { Id = 1, Name = "Man" };
            quote.DropoffLocation = new LocationModel() { Id = 1, Name = "Man" };
            quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });

            model = new QuoteModelView();
            try
            {
                if (model != null)
                {
                    var results = await _quoteservice.GetQuoteWithPriceAndReviews(quote);

                    var resultsview = ToResultsView(results);

                    return View(resultsview);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return View();
        }

        private List<ResultsView> ToResultsView(QuoteModel model)
        {
            List<ResultsView> domainModel = new List<ResultsView>();

            domainModel = model.Pricing != null ? model.Pricing.Select(x => new ResultsView
            {
                Address = x.PriceModel.BookingEntity.Address != null ? new AddressView {
                    Address1 = x.PriceModel.BookingEntity.Address.Address1,
                    Address2 = x.PriceModel.BookingEntity.Address.Address2,
                    City = x.PriceModel.BookingEntity.Address.City,
                    Country = x.PriceModel.BookingEntity.Address.Country,
                    County = x.PriceModel.BookingEntity.Address.County,
                    Number = x.PriceModel.BookingEntity.Address.Number,
                    Postcode = x.PriceModel.BookingEntity.Address.Postcode
                } : null,
                Price = x.Price,
                Company = x.PriceModel.BookingEntity.Name,
                CompanyLogo = x.PriceModel.BookingEntity.Image,
                Description = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "description").FirstOrDefault()!=null?x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "description").FirstOrDefault().Value:null,
                DistanceFromAirport = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "airportdistance").FirstOrDefault() != null ? (decimal?)Convert.ToDecimal(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "airportdistance").FirstOrDefault().Value):null,
                TransferTime = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "transfer").FirstOrDefault() != null ? (decimal?)Convert.ToDecimal(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "transfer").FirstOrDefault().Value) : null,
                IsRegularTransfers = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isregulartransfers").FirstOrDefault() != null ? (bool?)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isregulartransfers").FirstOrDefault().Value):null,
                IsFamilyFriendly = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isfamilyfriendly").FirstOrDefault() != null ? (bool?)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isfamilyfriendly").FirstOrDefault().Value):null,
                IsRetainKeys = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isretainkeys").FirstOrDefault() != null ?(bool?)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isretainkeys").FirstOrDefault().Value):null,
                Is24hSecurity = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "isretainkeys").FirstOrDefault() != null ? (bool?)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "is24security").FirstOrDefault().Value) : null,
                IsParkAndRide = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "parkandride").FirstOrDefault() != null ? (bool?)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "parkandride").FirstOrDefault().Value) : null,
                IsMeetAndGreet = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "meetandgreet").FirstOrDefault() != null ? (bool?)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "meetandgreet").FirstOrDefault().Value) : null,
                IsOnAirport = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "onairport").FirstOrDefault() != null ? (bool?)Convert.ToBoolean(x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "onairport").FirstOrDefault().Value) : null,
                Summary = x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "summary").FirstOrDefault() != null ? x.PriceModel.BookingEntity.Properties.Where(y => y.Key.ToLower() == "summary").FirstOrDefault().Value:null,
                Reviews = x.PriceModel.BookingEntity.Reviews!=null? x.PriceModel.BookingEntity.Reviews.Select(u=>new ReviewView {
                     ClientName = u.Author,
                     Subject = u.Subject,
                     PublicationDate= (DateTime)u.Created,
                     Review = u.Comments,
                     Rating = u.Rating
                }).ToList():null
            }).ToList() : null;

            return domainModel;
        }

        [HttpPost]
        [Route("index")]
        public async Task<ActionResult> Index(QuoteModelView model)
        {
            if (ModelState.IsValid)
            {
                return this.RedirectToAction("results", new RouteValueDictionary(model));
            }
            else
            {
                //## IT Gets all the airports
                var airports = await _airportservice.GetAll();
                ViewBag.airports = airports != null ? airports.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList() : null;
                return View(model);
            }
                
        }
    }
}