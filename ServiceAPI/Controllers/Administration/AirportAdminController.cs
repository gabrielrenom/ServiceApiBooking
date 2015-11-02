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
        }

        // GET: AirportAdmin/Details/5
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                RootBookingEntityModel airport = new RootBookingEntityModel();
                if (ModelState.IsValid)
                {
                    _airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _airportcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _airportcontroller.GetById(id);

                    result.TryGetContentValue(out airport);

                    if (airport != null)
                        return View(airport);
                }
            }
            catch
            {
                return View();
            }

            return View();
        }

        // GET: AirportAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AirportAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(RootBookingEntityModel model)
        {
            try
            {
              
                RootBookingEntityModel airport = new RootBookingEntityModel();
                if (ModelState.IsValid)
                {
                    _airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _airportcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _airportcontroller.Add(model);

                    result.TryGetContentValue(out airport);

                    if (airport!=null)
                        return RedirectToAction("Index");
                }
            }
            catch
            {
                return View("Create");
            }

            return View("Create");
        }

        // GET: AirportAdmin/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                RootBookingEntityModel airport = new RootBookingEntityModel();
                if (ModelState.IsValid)
                {
                    _airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _airportcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _airportcontroller.GetById(id);

                    result.TryGetContentValue(out airport);

                    if (airport != null)
                        return View(airport);
                }
            }
            catch
            {
                return View();
            }

            return View();
        }

        // POST: AirportAdmin/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, RootBookingEntityModel model)
        {
            try
            {                 
                bool airport = false;
                if (ModelState.IsValid)
                {
                    _airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _airportcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _airportcontroller.Update(model);

                    result.TryGetContentValue(out airport);

                    if (airport)
                        return RedirectToAction("Index");
                }
            }
            catch
            {
                return View("Edit");
            }

            return View("Edit");
        }

        // GET: AirportAdmin/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                bool isdeleted = false;
                    _airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _airportcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _airportcontroller.Delete(id);

                    result.TryGetContentValue(out isdeleted);

                    if (isdeleted)
                        return RedirectToAction("Index");
                
            }
            catch
            {
                return View();
            }

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
