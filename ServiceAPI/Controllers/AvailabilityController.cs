using ACP.Business;
using ACP.Business.Exceptions;
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using ServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Security;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceAPI.Controllers
{

    [RoutePrefix("api/v0.1/availability")]
    public class AvailabilityController : ApiController
    {
        private IAvailabilityService _availabilityservice;
        private IQuoteService _quoteservice;
        //
        public AvailabilityController(
            IAvailabilityService availabilityservice, 
            IQuoteService quoteservice)
        {
            _availabilityservice = availabilityservice;
            _quoteservice = quoteservice;
        }

        [HttpGet]
        [Route("gettbyavailability")]
        public async Task<HttpResponseMessage> GettByAvailability(AvailabilityModel model)
        {
            IList<AvailabilityModel> available = null;
            try
            {                
                available = await _availabilityservice.GetByAvailability(model);
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exceptionMessage);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, available, new JsonMediaTypeFormatter());
        }


        [HttpGet]
        [Route("gettbyavailabilitywithprice")]
        public async Task<HttpResponseMessage> GettByAvailabilityWithPrice(AvailabilityViewModel model)
        {
            IList<AvailabilityModel> available = null;
            IList<AvailabilityViewModel> listavailable = new List<AvailabilityViewModel>();
            try
            {
                available = await _availabilityservice.GetByAvailability(new AvailabilityModel {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    Status = new StatusModel {  StatusType= (ACP.Business.Enums.StatusType)model.StatusType}
                });
                if (available != null)
                {
 
                    foreach (var slot in available)
                    {
                        AvailabilityViewModel view = new AvailabilityViewModel();

                        var price = await _quoteservice.GetQuoteWithPriceByBookingEntityId(
                       slot.Slot.BookingEntityId, new QuoteModel
                       {
                           Dropoff = model.StartDate,
                           Pickup = model.EndDate
                       });

                        view.SlotId = slot.Id;
                        view.StatusType = (int)slot.Status.StatusType;
                        view.StartDate = model.StartDate;
                        view.EndDate = model.EndDate;
                        view.Price = price.Price;
                        view.CarPark = slot.Slot.BookingEntity.Name;
                        view.CarParkCode = slot.Slot.BookingEntity.Code;
                        view.Airport = slot.Slot.BookingEntity.RootBookingEntity.Name;
                        view.AirportCode = slot.Slot.BookingEntity.RootBookingEntity.Code;

                        listavailable.Add(view);
                    }
                  
                }
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exceptionMessage);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, listavailable, new JsonMediaTypeFormatter());
        }



        [HttpGet]
        [Route("getbyid")]
        public async Task<HttpResponseMessage> GettById(int id)
        {
            AvailabilityModel available = null;
            try
            {
                available = await _availabilityservice.GetById(id);
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exceptionMessage);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, available, new JsonMediaTypeFormatter());
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> Add(AvailabilityModel model)
        {
            AvailabilityModel available = null;
            try
            {
                available =await _availabilityservice.Add(model);
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exceptionMessage);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, available, new JsonMediaTypeFormatter());
        }

        [HttpPut]
        [Route("update")]
        public async Task<HttpResponseMessage> Update(AvailabilityModel model)
        {
            bool available = false;
            try
            {
                available = await _availabilityservice.Update(model);
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exceptionMessage);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, available, new JsonMediaTypeFormatter());
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            bool available = false;
            try
            {
                available = await _availabilityservice.Remove(id);
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exceptionMessage);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }

            return Request.CreateResponse(HttpStatusCode.Created, available, new JsonMediaTypeFormatter());
        }

    }
}
