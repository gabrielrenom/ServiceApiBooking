using ACP.Business.Models;
using NSubstitute;
using ServiceAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceAPI.Administration
{
    public class AirportAdminController : Controller
    {
        AirportController _airportcontroller;

        public AirportAdminController(AirportController airportcontroller)
        {
            _airportcontroller = airportcontroller;
        }

        // GET: AirportAdmin
        public async Task<ActionResult> Index()
        {
            List<RootBookingEntityModel> airports = new List<RootBookingEntityModel>();
            _airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _airportcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var result = await _airportcontroller.GetAll();

            result.TryGetContentValue<List<RootBookingEntityModel>>(out airports);

            return View(airports);

            return View();
        }

        // GET: AirportAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AirportAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AirportAdmin/Create
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

        // GET: AirportAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AirportAdmin/Edit/5
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

        // GET: AirportAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AirportAdmin/Delete/5
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
