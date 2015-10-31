using ACP.Business;
using ACP.Business.Exceptions;
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
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
    public class PricingAdminController : Controller
    {
        private PricingController _pricingcontroller;

        public PricingAdminController(PricingController pricingcontroller)
        {
            _pricingcontroller = pricingcontroller;
        }

        // GET: PricingAdmin
        public async Task<ActionResult> Index()
        {
            List<BookingPricingModel> prices = new List<BookingPricingModel>();

            try
            {
                _pricingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _pricingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _pricingcontroller.GetAll();

                result.TryGetContentValue(out prices);

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

            return View(prices);

        }

        // GET: PricingAdmin/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PricingAdmin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PricingAdmin/Create
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

        // GET: PricingAdmin/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PricingAdmin/Edit/5
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

        // GET: PricingAdmin/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PricingAdmin/Delete/5
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
