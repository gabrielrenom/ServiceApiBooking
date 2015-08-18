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
using System.Security;
using System.Threading.Tasks;
using System.Web.Http;

namespace ServiceAPI.Controllers
{
    public class AirportController : ApiController
    {
        private IRootBookingEntityService _airportservice;

        public AirportController(IRootBookingEntityService airportservice)
        {
            _airportservice = airportservice;
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GettById(int id)
        {
            AirportViewModel airport    =   null;
            try
            {                                
                airport = ToViewModel(_airportservice.GetById(id));
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

            return Request.CreateResponse(HttpStatusCode.Created, airport);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> Add(AirportViewModel model)
        {
            AirportViewModel airport = null;
            try
            {
                airport = _airportservice.Add(ToDataModel(model));
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

            return Request.CreateResponse(HttpStatusCode.Created, airport);
        }

        private AirportViewModel ToViewModel(RootBookingEntityModel dataModel)
        {
            AirportViewModel viewmodel = new AirportViewModel
            {
                Name = dataModel.Name,               
                Address = new AddressViewModel
                {

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
                    Name = dataModel.Status.Name
                }
            };            

            return viewmodel;
        }

        private RootBookingEntityModel ToDataModel(AirportViewModel dataModel)
        {
            RootBookingEntityModel model = new RootBookingEntityModel();
            
            model.Name = dataModel.Name;
            model.Address = new AddressModel
            {
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
                Name = dataModel.Status.Name
            };          

            return model;
        }
    }
}
