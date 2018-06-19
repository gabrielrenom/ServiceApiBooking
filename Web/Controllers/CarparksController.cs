
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using ACP.Commons;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
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
        private IEmailService _emailService;

        public CarparksController(
            IRootBookingEntityService airportservice,
            IQuoteService quoteservice,
            IEmailService emailService)
        {
            _airportservice = airportservice;
            _quoteservice = quoteservice;
            _emailService = emailService;
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
                            Pickup = DateHelper.ConvertToUKDateTime(model.ReturnDate),
                            Dropoff = DateHelper.ConvertToUKDateTime(model.DropOffDate),
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
            CultureInfo culture = new CultureInfo("en-UK");

            try
            {

                if (DateHelper.ConvertToUKDateTime(model.DropOffDate) > DateHelper.ConvertToUKDateTime(model.ReturnDate))
                {
                    ModelState.AddModelError("DropOffDate", "The date supplied is older than the Return Date");
                }

                if (DateHelper.ConvertToUKDateTime(model.DropOffDate) < DateHelper.ConvertToUKDateTime(DateTime.Today))
                {
                    ModelState.AddModelError("DropOffDate", "The date supplied can be in an earlier date");
                }

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
            catch (Exception ex){
                throw new System.ArgumentException(ex.ToString(), model.DropOffDate);
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

        [HttpGet]
        [Route("about")]
        public async Task<ActionResult> About()
        {
           
            return View();
        }

        [HttpGet]
        [Route("contactus")]
        public async Task<ActionResult> ContactUs()
        {

            return View(new Web.Models.ContactUsViewModel());
        }

        [HttpPost]
        [Route("contactus")]
        public async Task<ActionResult> ContactUs(ContactUsViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _emailService.SendEmail(
                    ConfigurationManager.AppSettings["eMail:Server"].ToString(),
                    model.Email,
                    ConfigurationManager.AppSettings["eMail:To"].ToString(),
                    model.Name,
                    model.Comments,
                    ConfigurationManager.AppSettings["eMail:Password"].ToString(),
                    Convert.ToInt32(ConfigurationManager.AppSettings["eMail:Port"]));

                model.Confirmation = "Your comments have been sent";
            }

            return View(model);
            
        }

        [HttpGet]
        [Route("faq")]
        public async Task<ActionResult> Faq()
        {
            return View();
        }
    }
}