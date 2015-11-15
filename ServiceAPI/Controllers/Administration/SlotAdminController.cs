using ACP.Business;
using ACP.Business.Exceptions;
using ACP.Business.Models;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ServiceAPI.Controllers
{
    public class SlotAdminController : Controller
    {
        private AvailabilityController _availabilitycontroller;
        private SlotController _slotcontroller;
        private CarParkController _carparkcontroller;

        public SlotAdminController(SlotController slotcontroller, CarParkController carparkcontroller, AvailabilityController availabilitycontroller)
        {
            _availabilitycontroller = availabilitycontroller;
            _slotcontroller = slotcontroller;
            _carparkcontroller = carparkcontroller;
        }

        public async Task<ActionResult> Availability(int id)
        {
            SlotModel slot  = new SlotModel();

            _slotcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _slotcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var result = await _slotcontroller.GetByIdWithAllAvailabilities(id);

            result.TryGetContentValue(out slot);
            if (slot != null)
            {
            }


            return View(slot);
        }

        public async Task<JsonResult> AddAvailability(AvailabilityModel model)
        {
            AvailabilityModel availability = new AvailabilityModel();

            _availabilitycontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _availabilitycontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var slot = await _availabilitycontroller.Add(model);
           
            slot.TryGetContentValue(out availability);
            if (availability != null)
            {
            }

            return Json(availability, JsonRequestBehavior.DenyGet);  
        }

        [HttpDelete]
        public async Task<JsonResult> RemoveAvailability(int id)
        {
            bool result = false;

            _availabilitycontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
            _availabilitycontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
            var slot = await _availabilitycontroller.Delete(id);

            slot.TryGetContentValue(out result);
            
            return Json(result);
        }
        private async Task LoadCarparks()
        {
            if (ViewBag.carparkslist == null)
            {
                var carparkslist = new List<SelectListItem>();
                List<BookingEntityModel> carparks = new List<BookingEntityModel>();
                _carparkcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _carparkcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _carparkcontroller.GetAll();
                result.TryGetContentValue(out carparks);
                if (carparks != null)
                {
                    foreach (var carpark in carparks)
                    {
                        carparkslist.Add(new SelectListItem() { Text = carpark.Name, Value = carpark.Id.ToString() });
                    }
                }
                ViewBag.carparkslist = carparkslist;
            }
        }

        // GET: SlotAdmin
        public async Task<ActionResult> Index()
        {
            await LoadCarparks();

            List<SlotModel> slots = new List<SlotModel>();

            try
            {
                _slotcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _slotcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _slotcontroller.GetAll();

                result.TryGetContentValue(out slots);

                slots.Add(new SlotModel());
                slots.Add(new SlotModel());
                slots.Add(new SlotModel());
                slots.Add(new SlotModel());
                slots.Add(new SlotModel());
                slots.Add(new SlotModel());
                slots.Add(new SlotModel());
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

            return View(slots);
        }

        // GET: SlotAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SlotAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SlotAdmin/Create
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

        // GET: SlotAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SlotAdmin/Edit/5
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

        // GET: SlotAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SlotAdmin/Delete/5
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
