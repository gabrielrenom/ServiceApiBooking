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
        private SlotController _slotcontroller;
        public SlotAdminController(SlotController slotcontroller)
        {
            _slotcontroller = slotcontroller;
        }

        // GET: SlotAdmin
        public async Task<ActionResult> Index()
        {
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
