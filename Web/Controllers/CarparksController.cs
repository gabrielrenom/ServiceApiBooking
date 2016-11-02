
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
                FillAirports();
            }
            catch (Exception ex)
            {

            }
            return View(new QuoteModelView { Airport = "", Discount = "", ReturnDate = "", DropOffDate = "" });
           
        }

        [HttpGet]
        [Route("results")]
        public async Task<ActionResult> Results(QuoteModelView model)
        {
            FillAirports();

            List<ResultsView> resultsview = new List<ResultsView>();
            ViewBag.Searched = model;
            if (ModelState.IsValid)
            {         
                //QuoteModel quote = new QuoteModel();
                //quote.Pickup = Convert.ToDateTime(model.ReturnDate);//new DateTime(2016, 12, 28, 18, 40, 00); 
                //quote.Dropoff = Convert.ToDateTime(model.DropOffDate); //new DateTime(2016, 12, 23, 16, 30, 00);
                //quote.PickupLocation = new LocationModel() { Id = Convert.ToInt32(model.Airport)};
                //quote.DropoffLocation = new LocationModel() { Id = Convert.ToInt32(model.Airport)};
                //quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });               
                try
                {
                    if (model != null)
                    {
                        var quote = new QuoteModel
                        {
                            Pickup = Convert.ToDateTime(model.ReturnDate),
                            Dropoff = Convert.ToDateTime(model.DropOffDate),
                            PickupLocation = new LocationModel() { Id = Convert.ToInt32(model.Airport) },
                            DropoffLocation = new LocationModel() { Id = Convert.ToInt32(model.Airport) },
                            BookingServices = new List<BookingServiceModel> { new BookingServiceModel { Name = "Carpark" } }
                        };

                        var results = await _quoteservice.GetQuoteWithPriceAndReviews(quote);

                        resultsview = ToResultsView(results, new QuoteModelView { Airport = model.Airport, Discount = model.Discount, DropOffDate = model.DropOffDate, ReturnDate = model.ReturnDate });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
       
                }
            }
            return View(resultsview);
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
                Reviews = x.PriceModel.BookingEntity.Reviews != null ? x.PriceModel.BookingEntity.Reviews.Select(u => new ReviewView
                {
                    ClientName = u.Author,
                    Subject = u.Subject,
                    PublicationDate = (DateTime)u.Created,
                    Review = u.Comments,
                    Rating = u.Rating
                }).ToList() : null,
                Quote = quote
            }).ToList() : null;
            
            return domainModel;
        }

        private async void FillAirports()
        {
            //## IT Gets all the airports
            var airports = await _airportservice.GetAll();

            ViewBag.airports = airports != null ? airports.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList() : null;
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

        [HttpPost]
        [Route("book")]
        public ActionResult book(QuoteModelView model)
        {
            if (ModelState.IsValid)
            {
            }
            else
            {
                //## IT Gets all the airports
               
            }

            return View(model);
        }

        [HttpPost]
        [Route("getquote")]
        public async Task<ActionResult> GetQuote(QuoteModelView model)
        {
            FillAirports();
            List<ResultsView> resultsview = new List<ResultsView>();
            ViewBag.Searched = model;
            if (ModelState.IsValid)
            {
                //QuoteModel quote = new QuoteModel();
                //quote.Pickup = Convert.ToDateTime(model.ReturnDate);//new DateTime(2016, 12, 28, 18, 40, 00); 
                //quote.Dropoff = Convert.ToDateTime(model.DropOffDate); //new DateTime(2016, 12, 23, 16, 30, 00);
                //quote.PickupLocation = new LocationModel() { Id = Convert.ToInt32(model.Airport)};
                //quote.DropoffLocation = new LocationModel() { Id = Convert.ToInt32(model.Airport)};
                //quote.BookingServices.Add(new BookingServiceModel() { Name = "Carpark" });               
                try
                {
                    if (model != null)
                    {
                        var results = await _quoteservice.GetQuoteWithPriceAndReviews(new QuoteModel
                        {
                            Pickup = Convert.ToDateTime(model.ReturnDate),
                            Dropoff = Convert.ToDateTime(model.DropOffDate),
                            PickupLocation = new LocationModel() { Id = Convert.ToInt32(model.Airport) },
                            DropoffLocation = new LocationModel() { Id = Convert.ToInt32(model.Airport) },
                            BookingServices = new List<BookingServiceModel> { new BookingServiceModel { Name = "Carpark" } }
                        });

                        resultsview = ToResultsView(results, new QuoteModelView { Airport = model.Airport, Discount = model.Discount, DropOffDate = model.DropOffDate, ReturnDate = model.ReturnDate });
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());

                }
            }
            return View(model);
        }

    }
}