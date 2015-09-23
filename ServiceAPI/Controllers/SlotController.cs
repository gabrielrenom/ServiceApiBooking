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
    [RoutePrefix("api/v0.1/Slotcontroller")]
    public class SlotController : BaseApiController
    {
        private ISlotService _Slotservice;

        public SlotController(ISlotService Slotservice)
        {
            _Slotservice = Slotservice;
        }

        [HttpGet]
        [Route("getbyid")]
        public async Task<HttpResponseMessage> GetById(int id)
        {
            SlotModel Slot = null;
            try
            {
                Slot = await _Slotservice.GetById(id);
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

            return Request.CreateResponse(HttpStatusCode.Created, Slot, new JsonMediaTypeFormatter());
        }

        [HttpGet]
        [Route("getall")]
        public async Task<HttpResponseMessage> GetAll()
        {
            IList<SlotModel> Slots = null;
            try
            {
                Slots = await _Slotservice.GetAll();
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

            return Request.CreateResponse(HttpStatusCode.Created, Slots, new JsonMediaTypeFormatter());
        }

        [HttpGet]
        [Route("getbyname")]
        public async Task<HttpResponseMessage> GetByName(int? number=null,string identifier=null)
        {
            SlotModel Slot = null;
            try
            {
                Slot = await _Slotservice.GetByNumberIdentifier(number,identifier);
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

            return Request.CreateResponse(HttpStatusCode.Created, Slot, new JsonMediaTypeFormatter());
        }

        [HttpGet]
        [Route("getallfree")]
        public async Task<HttpResponseMessage> GetAllFree()
        {
            IList<SlotModel> Slots = null;
            try
            {
                Slots = await _Slotservice.GetAllFree();
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

            return Request.CreateResponse(HttpStatusCode.Created, Slots, new JsonMediaTypeFormatter());
        }

        [HttpGet]
        [Route("getalloccupied")]
        public async Task<HttpResponseMessage> GetAllOccupied()
        {
            IList<SlotModel> Slots = null;
            try
            {
                Slots = await _Slotservice.GetAllOccupied();
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

            return Request.CreateResponse(HttpStatusCode.Created, Slots, new JsonMediaTypeFormatter());
        }

        [HttpGet]
        [Route("findSlotavailable")]
        public async Task<HttpResponseMessage> FindSlotAvailable(DateTime startdate, DateTime enddate, string airport)
        {
            IList<SlotModel> Slots = null;
            try
            {
                Slots = await _Slotservice.FindSlotAvailable(startdate, enddate, airport);
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

            return Request.CreateResponse(HttpStatusCode.Created, Slots, new JsonMediaTypeFormatter());
        }


        [HttpPost]
        [Route("add")]
        public async Task<HttpResponseMessage> Add(SlotModel model)
        {
            SlotModel Slot = null;
            try
            {
                Slot = await _Slotservice.Add(model);
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

            return Request.CreateResponse(HttpStatusCode.Created, Slot, new JsonMediaTypeFormatter());
        }

        [HttpPut]
        [Route("update")]
        public async Task<HttpResponseMessage> Update(SlotModel model)
        {
            bool Slot = false;
            try
            {
                Slot = await _Slotservice.Update(model);
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

            return Request.CreateResponse(HttpStatusCode.Created, Slot, new JsonMediaTypeFormatter());
        }

        [HttpDelete]
        [Route("delete")]
        public async Task<HttpResponseMessage> Delete(int id)
        {
            bool Slot = false;
            try
            {
                Slot = await _Slotservice.Remove(id);
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

            return Request.CreateResponse(HttpStatusCode.Created, Slot, new JsonMediaTypeFormatter());
        }


        private RootBookingPropertyViewModel ToViewModel(RootBookingEntityModel dataModel)
        {
            RootBookingPropertyViewModel viewmodel = new RootBookingPropertyViewModel
            {
                Id = dataModel.Id,
                Name = dataModel.Name,
                Address = new AddressViewModel
                {
                    Id = dataModel.Address.Id,
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
                    Id = dataModel.Status.Id,
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
                Id = dataModel.Status.Id,
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
