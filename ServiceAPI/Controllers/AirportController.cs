using ACP.Business;
using ACP.Business.Enums;
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
    [RoutePrefix("api/v0.1/airport")]
    public class AirportController : BaseApiController
    {
        private IRootBookingEntityService _airportservice;

        public AirportController(IRootBookingEntityService airportservice)
        {
            _airportservice = airportservice;
        }

        [HttpGet]
        [Route("getbyid")]
        public async Task<HttpResponseMessage> GetById(int id)
        {
            RootBookingEntityModel airport = null;
            try
            {
                airport = await _airportservice.GetById(id);
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

            return Request.CreateResponse(HttpStatusCode.Created, airport, new JsonMediaTypeFormatter());
        }

        [HttpGet]
        [Route("getall")]
        public async Task<HttpResponseMessage> GetAll()
        {
            IList<RootBookingEntityModel> airports = null;
            try
            {
                airports = await _airportservice.GetAll();
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

            return Request.CreateResponse(HttpStatusCode.Created, airports, new JsonMediaTypeFormatter());
        }

        [HttpGet]
        [Route("getbyname")]
        public async Task<HttpResponseMessage> GetByName(string name)
        {
            RootBookingEntityModel airport = null;
            try
            {
                airport = await _airportservice.GetByName(name);
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

            return Request.CreateResponse(HttpStatusCode.Created, airport, new JsonMediaTypeFormatter());
        }


        //[HttpGet]
        //[Route("getbyid")]
        //public async Task<HttpResponseMessage> GettById(int id)
        //{
        //    RootBookingPropertyViewModel airport    =   null;
        //    try
        //    {                                
        //        airport = ToViewModel(await _airportservice.GetById(id));
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

        //    return Request.CreateResponse(HttpStatusCode.Created, airport);
        //}

        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> Add(RootBookingEntityModel model)
        {
            RootBookingEntityModel airport = null;
            try
            {
                airport = await _airportservice.Add(model);
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

            return Request.CreateResponse(HttpStatusCode.Created, airport, new JsonMediaTypeFormatter());
        }

        [HttpPut]
        [Route("update")]
        public async Task<HttpResponseMessage> Update(RootBookingPropertyViewModel model)
        {
            bool airport = false;
            try
            {
                airport = await _airportservice.Update(ToDataModel(model));
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

            return Request.CreateResponse(HttpStatusCode.Created, airport, new JsonMediaTypeFormatter());
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            bool airport = false;
            try
            {
                airport =  await _airportservice.Remove(id);
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

            return Request.CreateResponse(HttpStatusCode.Created, airport, new JsonMediaTypeFormatter());
        }


        private RootBookingPropertyViewModel ToViewModel(RootBookingEntityModel dataModel)
        {
            RootBookingPropertyViewModel viewmodel = new RootBookingPropertyViewModel
            {
                Id = dataModel.Id,
                Name = dataModel.Name,               
                Address = new AddressViewModel
                {
                    Id=dataModel.Address.Id,
                    Address1 = dataModel.Address.Address1,
                    Address2 = dataModel.Address.Address2,
                    Country = dataModel.Address.Country,
                    County = dataModel.Address.County,                 
                    Number = dataModel.Address.Number,
                    Postcode = dataModel.Address.Postcode
                },
                AddressId = dataModel.AddressId,
                StatusId = dataModel.StatusId,
                Telephone = dataModel.Telephone,
                Status = new StatusViewModel
                {                    
                    Id=dataModel.Status.Id,                    
                    StatusType = (Enums.StatusType)dataModel.Status.StatusType
                }
            };            

            return viewmodel;
        }

        private RootBookingEntityModel ToDataModel(RootBookingPropertyViewModel dataModel)
        {
            RootBookingEntityModel model = new RootBookingEntityModel();
            model.Id = dataModel.Id;
            model.Name = dataModel.Name;
            model.Address = new AddressModel
            {
                Id = dataModel.Address.Id,
                Address1 = dataModel.Address.Address1,
                Address2 = dataModel.Address.Address2,
                Country = dataModel.Address.Country,
                County = dataModel.Address.County,
                Number = dataModel.Address.Number,
                Postcode = dataModel.Address.Postcode
            };

            model.AddressId = dataModel.AddressId;
            model.StatusId = dataModel.StatusId;
            model.Telephone = dataModel.Telephone;
            model.Status = new StatusModel
            {
                Id= dataModel.Status.Id,
                StatusType = (StatusType)dataModel.Status.StatusType
            };
            if (dataModel.Id == 0)
            {
                model.Created = DateTime.Now;
                model.CreatedBy = "local";
            }

            model.Modified = DateTime.Now;
            model.ModifiedBy = "local";

            return model;
        }
    }
}
