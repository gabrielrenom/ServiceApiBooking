using ACP.Business;
using ACP.Business.APIs.PP;
using ACP.Business.APIs.PP.Models.Airports;
using ACP.Business.Exceptions;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceAPI.Controllers
{
    [RoutePrefix("api/v0.1/purpleparking")]
    public class PurpleParkingController : ApiController
    {
        private IRootBookingEntityService _airportservice;
        private IStatusService _statusservice;
        private IPurpleParking _purpleparkingapi;

        public PurpleParkingController(IRootBookingEntityService airportservice,  IStatusService statusservice)
        {
            _airportservice = airportservice;
            _statusservice = statusservice;

            _purpleparkingapi = new PurpleParking(airportservice, statusservice);
        }

        [HttpGet]
        [Route("getallairports")]
        public async Task<HttpResponseMessage> GetByAllAirports()
        {
            List<responseAirportCarpark> airports = new List<responseAirportCarpark>();
            try
            {
                airports = await _purpleparkingapi.GetAirports();
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

            return Request.CreateResponse(HttpStatusCode.Created, airports);
        }

        [HttpGet]
        [Route("getallcarparks")]
        public async Task<HttpResponseMessage> GetByAllCarparks()
        {
            List<responseAirportCarPark> carparks = new List<responseAirportCarPark>();
            try
            {
                 carparks   =   await _purpleparkingapi.GetCarParks();
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

            return Request.CreateResponse(HttpStatusCode.Created, carparks);
        }

        [HttpGet]
        [Route("getallcarparks")]
        public async Task<HttpResponseMessage> FillAirports()
        {
            bool result = false;

            try
            {
                result = await _purpleparkingapi.FillAirports();
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

            return Request.CreateResponse(HttpStatusCode.Created, result);
        }
    }
}
