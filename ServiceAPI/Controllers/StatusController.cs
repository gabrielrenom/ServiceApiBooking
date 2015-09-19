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
    [RoutePrefix("api/v0.1/status")]
    public class StatusController : BaseApiController
    {
        private IStatusService _statusservice;

        public StatusController(IStatusService statusservice)
        {
            _statusservice = statusservice;
        }

        [HttpGet]
        [Route("getbyname")]
        public async Task<HttpResponseMessage> GetByName(ACP.Business.Enums.StatusType statusname)
        {
            StatusModel status = null;
            try
            {
                status = await _statusservice.GetByName(statusname);
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

            return Request.CreateResponse(HttpStatusCode.Created, status, new JsonMediaTypeFormatter());
        }

        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> Add(StatusModel status)
        {            
            try
            {
                status = await _statusservice.Add(status);
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

            return Request.CreateResponse(HttpStatusCode.Created, status, new JsonMediaTypeFormatter());
        }
    }
}
