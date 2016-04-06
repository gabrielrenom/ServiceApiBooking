using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class IndexController : Controller
    {
        private IRootBookingEntityService _airportservice;

        public IndexController(IRootBookingEntityService airportservice)
        {
            _airportservice = airportservice;
        }
        
        [HttpGet]
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

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetQuote()
        {
            return View();
        }
    }
}