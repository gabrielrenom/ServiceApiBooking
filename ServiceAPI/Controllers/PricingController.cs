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
    /// <summary>
    /// The pricing controller is in charge on do all the CRUD opreations of the prices for teh parkings.
    /// Some extra actions hasve been added to allow the user to have better interaction.
    /// Prices can be setup by days and times.
    /// </summary>
    [RoutePrefix("api/v0.1/pricing")]
    public class PricingController : BaseApiController
    {
        //private IPricingService _pricingservice;
        private IBookingPricingService _bookingpricingservice;

        /// <summary>
        /// The pricing controller will interact with the pricing services(quotes) as well as the carparkpark prices. 
        /// </summary>
        /// <param name="pricingservice"></param>
        /// <param name="bookingpricingservice"></param>
        //public PricingController(IPricingService pricingservice, IBookingPricingService bookingpricingservice)
        //{
        //    _pricingservice = pricingservice;
        //    _bookingpricingservice = bookingpricingservice;
        //}

        public PricingController(IBookingPricingService bookingpricingservice)
        {
           
            _bookingpricingservice = bookingpricingservice;
        }

        /// <summary>
        /// It get the price for a particular quote. Very useful for client side queries.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>It will get the quote with price.</returns>
        //[HttpGet]
        //[Route("getprice")]
        //public async Task<HttpResponseMessage> GetPrice(QuoteModel model)
        //{
        //    PriceQuoteModel quote = null;
        //    try
        //    {
        //        quote = await _pricingservice.GetPrice(model);
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        Trace.TraceError(ex.Message);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }
        //    catch (SecurityException ex)
        //    {
        //        Trace.TraceError(ex.Message);
        //        return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
        //    }
        //    catch (ItemNotFoundException ex)
        //    {
        //        Trace.TraceError(ex.Message);
        //        return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
        //    }
        //    catch (ValidationErrorsException ex)
        //    {
        //        var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

        //        var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

        //        Trace.TraceError(exceptionMessage);
        //        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exceptionMessage);
        //    }
        //    catch (Exception ex)
        //    {
        //        Trace.TraceError(ex.Message);
        //        return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.Created, quote, new JsonMediaTypeFormatter());
        //}

        /// <summary>
        /// It will get all teh prices from days and times.
        /// </summary>
        /// <returns>It returns all results if any.</returns>
        [HttpGet]
        [Route("getall")]
        public async Task<HttpResponseMessage> GetAll()
        {
            IList<BookingPricingModel> prices = null;
            try
            {
                prices = await _bookingpricingservice.GetAll();
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

            return Request.CreateResponse(HttpStatusCode.Created, prices, new JsonMediaTypeFormatter());
        }

        /// <summary>
        /// It will get all the prices with days from a particular carpark by passing the carparkid.
        /// </summary>
        /// <param name="carpoarkid"></param>
        /// <returns>It returns all results if any.</returns>
        [HttpGet]
        [Route("getallwithdays")]
        public async Task<HttpResponseMessage> GetAllPricesWithDays(int carpoarkid)
        {
            IList<BookingPricingModel> prices = null;
            try
            {
                prices = await _bookingpricingservice.GetAllPricesWithDays(carpoarkid);
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

            return Request.CreateResponse(HttpStatusCode.Created, prices, new JsonMediaTypeFormatter());
        }

        /// <summary>
        /// It will get all the prices with days and times from a particular carpark by passing the carparkid.
        /// </summary>
        /// <param name="carpoarkid"></param>
        /// <returns>It returns all results if any.</returns>
        [HttpGet]
        [Route("getallwithdaysandtimes")]
        public async Task<HttpResponseMessage> GetAllPricesWithDaysAndTimes(int carpoarkid)
        {
            IList<BookingPricingModel> prices = null;
            try
            {
                prices = await _bookingpricingservice.GetAllPricesWithDaysAndTimes(carpoarkid);
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

            return Request.CreateResponse(HttpStatusCode.Created, prices, new JsonMediaTypeFormatter());
        }

        /// <summary>
        /// It will add the prices with days from a particular carpark by passing the carparkid and model.
        /// </summary>
        /// <param name="carparkid"></param>
        /// <param name="model"></param>
        /// <returns>It returns true/false if it has been sucessful.</returns>
        [HttpPost]
        [Route("addpriceswithdays")]
        public async Task<HttpResponseMessage> AddPricesWithDays(int carparkid, IList<BookingPricingModel> model)
        {
            bool bookingpricing = false;
            try
            {
                 bookingpricing = await _bookingpricingservice.AddPricesWithDays(carparkid,model);
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

            return Request.CreateResponse(HttpStatusCode.Created, bookingpricing, new JsonMediaTypeFormatter());
        }

        /// <summary>
        /// It will add the prices with days and times from a particular carpark by passing the carparkid and model.
        /// </summary>
        /// <param name="carparkid"></param>
        /// <param name="model"></param>
        /// <returns>It returns true/false if it has been sucessful.</returns>
        [HttpPost]
        [Route("addpriceswithdaysandtimes")]
        public async Task<HttpResponseMessage> AddPricesWithDaysAndTimes(int carparkid, IList<BookingPricingModel> model)
        {
            bool bookingpricing = false;
            try
            {
                bookingpricing = await _bookingpricingservice.AddPricesWithDaysAndTimes(carparkid, model);
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

            return Request.CreateResponse(HttpStatusCode.Created, bookingpricing, new JsonMediaTypeFormatter());
        }

        /// <summary>
        /// It will update the prices with days from a particular carpark by passing the carparkid and model.
        /// </summary>
        /// <param name="carparkid"></param>
        /// <param name="model"></param>
        /// <returns>It returns true/false if it has been sucessful.</returns>
        [HttpPut]
        [Route("updatepriceswithdays")]
        public async Task<HttpResponseMessage> UpdatePricesWithDays(int carparkid, IList<BookingPricingModel> model)
        {
            bool bookingpricing = false;
            try
            {
                bookingpricing = await _bookingpricingservice.UpdatePricesWithDays(carparkid, model);
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

            return Request.CreateResponse(HttpStatusCode.Created, bookingpricing, new JsonMediaTypeFormatter());
        }

        /// <summary>
        /// It will delete the prices in cascade by passing the id of a particular price.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>It returns true/false if it has been sucessful.</returns>
        [HttpDelete]
        [Route("delete")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            bool bookingpricing = false;
            try
            {
                bookingpricing = await _bookingpricingservice.DeleteById(id);
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

            return Request.CreateResponse(HttpStatusCode.Created, bookingpricing, new JsonMediaTypeFormatter());
        }
    }
}
