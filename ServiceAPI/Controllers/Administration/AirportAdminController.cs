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
        StatusController _statuscontroller;

        public AirportAdminController(AirportController airportcontroller, StatusController statuscontroller)
        {
            _airportcontroller = airportcontroller;
            _statuscontroller = statuscontroller;
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
                    {
                        return View(airport);
                    }
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
                model.StatusId = await GetActiveStatusId();

                RootBookingEntityModel airport = new RootBookingEntityModel();
                if (ModelState.IsValid)
                {
                    _airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _airportcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _airportcontroller.Add(model);

                    result.TryGetContentValue(out airport);

                    if (airport != null)
                        return RedirectToAction("Index");
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
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

                    //airport.Status = GetStatusModel(airport.Id);

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
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View("Edit");
            }

            return View("Edit");
        }

        private async Task<int> GetActiveStatusId()
        {
            StatusModel statusmodel = new StatusModel();
            _statuscontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _statuscontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var status = await _statuscontroller.GetByName(ACP.Business.Enums.StatusType.Active);

            status.TryGetContentValue(out statusmodel);

            return statusmodel.Id;
        }

        private async Task<StatusModel> GetStatusModel()
        {
            StatusModel statusmodel = new StatusModel();
            _statuscontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _statuscontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var status = await _statuscontroller.GetByName(ACP.Business.Enums.StatusType.Active);

            status.TryGetContentValue(out statusmodel);

            return statusmodel;
        }

        // GET: AirportAdmin/Delete/5
        public async Task<ActionResult> Delete(int id)
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
                    {
                        return View(airport);
                    }
                }
            }
            catch
            {
                return View();
            }

            return View();
        }
    

        // POST: AirportAdmin/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
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
    }
}
