using ACP.Business;
using ACP.Business.Exceptions;
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceAPI.Controllers
{
    public class CarparkAdminController : Controller
    {
        private CarParkController _carparkcontroller;
        private AirportController _airportcontroller;

        public CarparkAdminController(CarParkController carparkcontroller, AirportController airportcontroller)
        {
            _carparkcontroller = carparkcontroller;
            _airportcontroller = airportcontroller;
        }

        // GET: CarparkAdmin
        public async Task<ActionResult> Index()
        {
            List<BookingEntityModel> carparks = new List<BookingEntityModel>();
            
            try
            {
                _carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _carparkcontroller.GetAll();

                result.TryGetContentValue(out carparks);
                
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return View(carparks);

        }

        // GET: CarparkAdmin/Details/5
        public async Task<ActionResult> Details(int id)
        {
            BookingEntityModel carpark = new BookingEntityModel();

            try
            {
                _carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _carparkcontroller.GettById(id);

                result.TryGetContentValue(out carpark);

            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return View(carpark);

        }

        // GET: CarparkAdmin/Create
        public async Task<ActionResult> Create()
        {
            await FillDropBoxes();

            return View();
        }

        // POST: CarparkAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(BookingEntityModel model, FormCollection collection)
        {
            bool hasBeenCreated = false;
            try
            {
                if (ModelState.IsValid)
                {
                    _carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    model.RootBookEntityId = Convert.ToInt32(collection["airportlist"]);
                    var result = await _carparkcontroller.Add(model);

                    result.TryGetContentValue(out hasBeenCreated);

                    if (hasBeenCreated)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        await FillDropBoxes();
                        return View(model);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(model);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            await FillDropBoxes();
            return View(model);
        }

        // GET: CarparkAdmin/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            BookingEntityModel carpark = new BookingEntityModel();

            try
            {
                _carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _carparkcontroller.GettById(id);

                result.TryGetContentValue(out carpark);

                await FillDropBoxes();

            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            await FillDropBoxes();
            return View(carpark);

        }

        // POST: CarparkAdmin/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(BookingEntityModel model, FormCollection collection)
        {
            BookingEntityModel carpark = new BookingEntityModel();
            bool hasbeenupdated = false;

            try
            {
                if (ModelState.IsValid)
                {
                    _carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _carparkcontroller.GettById(model.Id);

                    result.TryGetContentValue(out carpark);

                    carpark.Code = model.Code;
                    carpark.Comission = model.Comission;
                    carpark.Name = model.Name;
                    carpark.Sameday = model.Sameday;
                    carpark.Price = model.Price;
                    carpark.RootBookEntityId = Convert.ToInt32(collection["airport"]);
                    if (carpark.Address!=null)
                    {
                        carpark.Address.Address1 = model.Address.Address1;
                        carpark.Address.Address2 = model.Address.Address2;
                        carpark.Address.City = model.Address.City;
                        carpark.Address.Country = model.Address.Country;
                        carpark.Address.County = model.Address.County;
                        carpark.Address.Number = model.Address.Number;
                        carpark.Address.Postcode = model.Address.Postcode;
                    }

                    var updateresult = await _carparkcontroller.Update(carpark);

                    updateresult.TryGetContentValue(out hasbeenupdated);

                    if (!hasbeenupdated)
                    {
                        await FillDropBoxes();
                        return View(model);
                    }
                }
                else
                {
                    await FillDropBoxes();
                    return View(model);
                }

            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return RedirectToAction("Index");

        }

        // GET: CarparkAdmin/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            BookingEntityModel carpark = new BookingEntityModel();

            try
            {
                _carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _carparkcontroller.GettById(id);

                result.TryGetContentValue(out carpark);

            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return View(carpark);

        }

        // POST: CarparkAdmin/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            bool carpark = false;

            try
            {
                _carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _carparkcontroller.Delete(id);

                result.TryGetContentValue(out carpark);

                if (!carpark)
                {
                    return View();
                }
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }

            return RedirectToAction("Index");


        }

        private async Task FillDropBoxes()
        {
            
            var airportlist = new List<SelectListItem>();
            List<RootBookingEntityModel> airports = new List<RootBookingEntityModel>();
            _airportcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _airportcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var airportsresult = await _airportcontroller.GetAll();
            airportsresult.TryGetContentValue(out airports);
            if (airports != null)
            {
                foreach (var airport in airports)
                {
                    airportlist.Add(new SelectListItem() { Text = airport.Name, Value = airport.Id.ToString() });
                }
            }
            ViewBag.airportlist = airportlist;
        }
    }
}
