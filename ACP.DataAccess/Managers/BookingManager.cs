using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Managers
{
    public class BookingManager : BaseACPManager<BookingModel, Booking>, IBookingManager
    {
        public BookingManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public override Task<BookingModel> AddAsync(BookingModel domainModel)
        {
            return base.AddAsync(domainModel);
        }

        public override Booking ToDataModel(BookingModel domainModel, Booking dataModel = null)
        {
            if (dataModel == null)
                dataModel = new Booking();

            dataModel.Created = domainModel.Created;
            dataModel.Id = domainModel.Id;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.StartDate = domainModel.StartDate;
            dataModel.EndDate = domainModel.EndDate;
            dataModel.Status =(Data.Enums.StatusType) domainModel.Status;
            dataModel.AgentReference = domainModel.AgentReference;
            dataModel.Cost = domainModel.Cost;
            dataModel.BookingReference = GenerateReference();

            //dataModel.Car = domainModel.Car != null ? new Car
            //{
            //    Colour = domainModel.Car.Colour,
            //    Id = domainModel.Car.Id,
            //    Registration = domainModel.Car.Registration,
            //    Model = domainModel.Car.Model,
            //    Make = domainModel.Car.Make,
            //    Created = domainModel.Car.Created,
            //    CreatedBy = domainModel.Car.CreatedBy,
            //    ModifiedBy = domainModel.Car.ModifiedBy,
            //    Modified = domainModel.Car.Modified,
            //    User = domainModel.Car.User != null ? new User
            //    {
            //        Address = null,                    
            //        Created = domainModel.Car.User.Created,
            //        CreatedBy = domainModel.Car.User.CreatedBy,
            //        ModifiedBy = domainModel.Car.User.ModifiedBy,
            //        Modified = domainModel.Car.User.Modified,
            //    } : null,
            //} : null;
            dataModel.CustomerId = domainModel.Id;
            dataModel.Customer = domainModel.Customer != null ? new Customer
            {
                Id = domainModel.Customer.Id,                
                Created = domainModel.Customer.Created,
                CreatedBy = domainModel.Customer.CreatedBy,
                ModifiedBy = domainModel.Customer.ModifiedBy,
                Modified = domainModel.Customer.Modified,
                Address = domainModel.Customer.Address != null ? new Address
                {
                    Created = domainModel.Customer.Address.Created,
                    CreatedBy = domainModel.Customer.Address.CreatedBy,
                    ModifiedBy = domainModel.Customer.Address.ModifiedBy,
                    Modified = domainModel.Customer.Address.Modified,
                } : null,
            } : null;
            //dataModel.Extras = domainModel.Extras != null ? domainModel.Extras.Select(x => new Extra {
            //    Created = x.Created,
            //    CreatedBy = x.CreatedBy,
            //    ModifiedBy = x.ModifiedBy,
            //    Modified = x.Modified,
            //}).ToList() : null;
            //dataModel.Message = domainModel.Message;
            
            //dataModel.Payments = domainModel.Payments != null ? domainModel.Payments.Select(x => new Payment
            //{
            //    Created = x.Created,
            //    CreatedBy = x.CreatedBy,
            //    ModifiedBy = x.ModifiedBy,
            //    Modified = x.Modified,
            //}).ToList() : null;
            //dataModel.Price = domainModel.Price;
            //dataModel.SourceCode = domainModel.SourceCode;
            dataModel.TravelDetailsId = domainModel.TravelDetailsId;
            dataModel.TravelDetails = domainModel.TravelDetails != null ? new TravelDetails
            {
                Id= domainModel.TravelDetails.Id,
                OutboundDate = domainModel.TravelDetails.OutboundDate,
                ReturnDate = domainModel.TravelDetails.ReturnDate,
                Created = domainModel.TravelDetails.Created,
                CreatedBy = domainModel.TravelDetails.CreatedBy,
                ModifiedBy = domainModel.TravelDetails.ModifiedBy,
                Modified = domainModel.TravelDetails.Modified,
            } : null;
            //dataModel.User = domainModel.User != null ? new User
            //{
            //    Created = domainModel.User.Created,
            //    CreatedBy = domainModel.User.CreatedBy,
            //    ModifiedBy = domainModel.User.ModifiedBy,
            //    Modified = domainModel.User.Modified,
            //} : null;           
            return dataModel;
        }

        private string GenerateReference()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
    }
}
