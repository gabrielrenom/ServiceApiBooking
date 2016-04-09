
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

        public CarparksController(IRootBookingEntityService airportservice)
        {
            _airportservice = airportservice;
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
            return View(new QuoteModelView { Airport="", Discount="", ReturnDate="", DropOffDate= "" });            
        }

        [HttpGet]
        [Route("results")]
        public async Task<ActionResult> Results(QuoteModelView model)
        {
            return View();
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