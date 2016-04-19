
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
                    var results = await _quoteservice.GetQuoteWithPrice(quote);

                    var resultsview = ToResultsView(results);
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

            domainModel = model.BookingItems != null ? model.BookingItems.Select(x => new ResultsView
            {
                IsRegularTransfers = x.Transfers != null ? true : false
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