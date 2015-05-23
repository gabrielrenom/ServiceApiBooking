﻿using ACP.Business;
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
using System.Security;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceAPI.Controllers
{
    public class ServiceController : ApiController
    {
        readonly IBookingServiceService _serviceService;

        public ServiceController(IBookingServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        // GET: api/Service
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Service/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Service

        public void Post([FromBody] BookingServiceViewModel model)
        {
            BookingServiceModel service;

            try
            {
                service = new BookingServiceModel();

                service.Name = model.Name;
                //service.CreatedBy = userName;
                service.Created = DateTime.UtcNow;

                service =  _serviceService.Add(service);
            }
            catch (HttpRequestException ex)
            {
                Trace.TraceError(ex.Message);
                //return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            catch (SecurityException ex)
            {
                Trace.TraceError(ex.Message);
                //return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ex.Message);
            }
            catch (ItemNotFoundException ex)
            {
                Trace.TraceError(ex.Message);
                //return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
            catch (ValidationErrorsException ex)
            {
                var errorMessages = ex.ValidationErrors.Select(x => x.ErrorMessage);

                var exceptionMessage = string.Concat("The request is invalid: ", string.Join("; ", errorMessages));

                Trace.TraceError(exceptionMessage);
                //return Request.CreateErrorResponse(HttpStatusCode.BadRequest, exceptionMessage);
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);
                //return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
            //return Request.CreateResponse(HttpStatusCode.Created, service);
        }

        //public async Task<HttpResponseMessage> Post([FromBody] ServiceViewModel model)
        //{
        //    ServiceModel service;

        //    try
        //    {
        //        service = new ServiceModel();

        //        service.Name = model.Name;
        //        //service.CreatedBy = userName;
        //        service.Created = DateTime.UtcNow;

        //        service = await _serviceService.AddAsync(service);
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
        //    return Request.CreateResponse(HttpStatusCode.Created, service);
        //}

        // PUT: api/Service/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Service/5
        public void Delete(int id)
        {
        }
    }
}
