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
        public async Task<ActionResult> Details(int id)
        {
            SlotModel slots = new SlotModel();

            try
            {
                _slotcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _slotcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _slotcontroller.GetById(id);

                result.TryGetContentValue(out slots);
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

        // GET: SlotAdmin/Create
        public async Task<ActionResult> Create()
        {
            await LoadCarparks();
            return View();
        }

        // POST: SlotAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(SlotModel model)
        {
            string localuser = "";
            SlotModel sresult;
            try
            {
                if (ModelState.IsValid)
                {
                    _slotcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _slotcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _slotcontroller.Add(model);

                    result.TryGetContentValue(out sresult);

                    if (sresult!=null)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }

            return View();
        }
        // GET: SlotAdmin/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            SlotModel slot = new SlotModel();

            try
            {
                _slotcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _slotcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _slotcontroller.GetById(id);

                result.TryGetContentValue(out slot);

                await LoadCarparks();

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

            return View(slot);

        }

        // POST: SlotAdmin/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(SlotModel model)
        {
            string localuser = "";

            bool bresult=false;
            try
            {
                if (ModelState.IsValid)
                {
                    _slotcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _slotcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _slotcontroller.Update(model);

                    result.TryGetContentValue(out bresult);

                    if (bresult)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }

            return View();
        }

        // GET: SlotAdmin/Delete/5
    
        public async Task<ActionResult> Delete(int id)
        {
            SlotModel sresult;
            try
            {
                    _slotcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _slotcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _slotcontroller.GetById(id);

                    result.TryGetContentValue(out sresult);

                    if (sresult != null)
                    {
                        return View(sresult);
                    }

                
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }
            return View();
        }

        // POST: SlotAdmin/Delete/5
       // [HttpPost]
        public async Task<ActionResult> DeleteSlot(int id)
        {
            string localuser = "";

            bool bresult = false;
            try
            {
                if (ModelState.IsValid)
                {
                    _slotcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _slotcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _slotcontroller.Delete(id);

                    result.TryGetContentValue(out bresult);

                    if (bresult)
                    {
                        return RedirectToAction("Index");
                    }

                }
            }
            catch (Exception ex)
            {
                return View(ex.ToString());
            }

            return View();
        }
    }
}
