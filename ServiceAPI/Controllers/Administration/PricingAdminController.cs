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
        private CarParkController _carparkcontroller;

        public PricingAdminController(PricingController pricingcontroller, CarParkController carparkcontroller)
        {
            _pricingcontroller = pricingcontroller;
            _carparkcontroller = carparkcontroller;
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
                await FillDropBoxes();
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
        public async Task<ActionResult> Details(int id)
        {
            BookingPricingModel prices = new BookingPricingModel();
         
            try
            {
                if (ModelState.IsValid)
                {
                    _pricingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _pricingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _pricingcontroller.GetById(id);

                    result.TryGetContentValue(out prices);
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

            return View(prices);
        }

        // GET: PricingAdmin/Create
        public async  Task<ActionResult> Create()
        {
            await FillDropBoxes();

            return View();
        }

        // POST: PricingAdmin/Create
        [HttpPost]
        public async Task<ActionResult> Create(BookingPricingModel model, FormCollection collection)
        {
            List<BookingPricingModel> prices = new List<BookingPricingModel>();
            string localuser = "";
            bool bresult = false;
            try
            {
                if (ModelState.IsValid)
                {
                    int carparkid = Convert.ToInt32(collection["carpark"]);
                    List <DayPriceModel> days = new List<DayPriceModel>();
                    for (int i = 1;i < 31; days.Add(new DayPriceModel { Day = i, Dayprice = Convert.ToDecimal(collection[Convert.ToString(i++)]), Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser }))  ;
                    prices.Add(new BookingPricingModel
                    {
                         BookingEntityId= carparkid,
                         Name = model.Name,
                         Start = model.Start,
                         End = model.End,
                         DayPrices = days,                          
                    });

                    _pricingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _pricingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _pricingcontroller.AddPricesWithDays(carparkid, prices);

                    result.TryGetContentValue(out bresult);

                    if (bresult)
                    {
                       return RedirectToAction("Index");
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
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            await FillDropBoxes();
            return View();

        }

        // GET: PricingAdmin/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            BookingPricingModel prices = new BookingPricingModel();
            string localuser = "";
            bool bresult = false;
            try
            {
                if (ModelState.IsValid)
                {
                    _pricingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _pricingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _pricingcontroller.GetById(id);

                    result.TryGetContentValue(out prices);
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

            return View(prices);
        }

        // POST: PricingAdmin/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, BookingPricingModel model,FormCollection collection)
        {
            BookingPricingModel prices = new BookingPricingModel();
            string localuser = "";
            bool bresult = false;
            try
            {
                if (ModelState.IsValid)
                {
                    _pricingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _pricingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();

                    var result = await _pricingcontroller.GetById(id);
                    result.TryGetContentValue(out prices);

                    prices.Start = model.Start;
                    prices.End = model.End;
                    prices.Name = model.Name;

                    if (prices.DayPrices == null)
                        prices.DayPrices = new List<DayPriceModel>();

                    for (int i = 1; i < 31; i++)
                    {
                        if (prices.DayPrices.Where(x => x.Day == i).FirstOrDefault() == null)
                        {
                            prices.DayPrices.Add(new DayPriceModel { Day = i, Dayprice = Convert.ToDecimal(collection[Convert.ToString(i++)]), Created = DateTime.Now, Modified = DateTime.Now, CreatedBy = localuser });
                        }
                        else
                        {
                            prices.DayPrices.Where(x => x.Day == i).FirstOrDefault().Dayprice= Convert.ToDecimal(collection[Convert.ToString(i++)]);                            
                        }
                    }
                   
                    var updateresult = await _pricingcontroller.UpdatePriceWithDays(prices);

                    updateresult.TryGetContentValue(out bresult);

                    if (bresult)
                    {
                       return RedirectToAction("Index");
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
                return View(ex.Message);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return View(ex.Message);
            }
            return View();
        }

        // GET: PricingAdmin/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            BookingPricingModel prices = new BookingPricingModel();

            try
            {
                if (ModelState.IsValid)
                {
                    _pricingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                    _pricingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                    var result = await _pricingcontroller.GetById(id);

                    result.TryGetContentValue(out prices);
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

            return View(prices);

        }

        // POST: PricingAdmin/Delete/5
        [HttpPost]
        public async  Task<ActionResult> Delete(int id, FormCollection collection)
        {
            bool bresult = false;

            try
            {
                _pricingcontroller.Request = Substitute.For<HttpRequestMessage>();  // using nSubstitute
                _pricingcontroller.Configuration = Substitute.For<System.Web.Http.HttpConfiguration>();
                var result = await _pricingcontroller.Delete(id);

                result.TryGetContentValue(out bresult);

                if (bresult)
                {
                    return RedirectToAction("Index");
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

            return View();
        }

        private async Task FillDropBoxes()
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
}
