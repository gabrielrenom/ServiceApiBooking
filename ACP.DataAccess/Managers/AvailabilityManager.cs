using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Data;
using ACP.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Managers
{
    public class AvailabilityManager : BaseACPManager<AvailabilityModel, Availability>, IAvailabilityManager
    {
        public AvailabilityManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public override AvailabilityModel Add(AvailabilityModel domainModel)
        {
            return base.Add(domainModel);
        }

        protected override IEnumerable<AvailabilityModel> GetListIncluding(Func<Availability, bool> where, params System.Linq.Expressions.Expression<Func<Availability, object>>[] navigationProperties)
        {
            return base.GetListIncluding(where, navigationProperties);
        }

        public IList<AvailabilityModel> FindAvailability(Func<Availability, bool> where)
        {
            return GetListIncluding(where, x=>x.Slot,x=>x.Slot.BookingEntity,x=>x.Slot.BookingEntity.RootBookingEntity).ToList();
            //return GetListIncluding(where, x => x.Status, x => x.Slot, x => x.Slot.BookingEntity).ToList()
            //return GetListIncluding(x=>x.StartDate >=  && x.StartDate <= endDate3, x => x.Status, x).ToList();
        }

        public IList<AvailabilityModel> FindAvailabilityWithPrices(Func<Availability, bool> where)
        {
           

            return GetListIncluding(where).ToList();
            //return GetListIncluding(x=>x.StartDate >=  && x.StartDate <= endDate3, x => x.Status, x).ToList();
        }       

        public IEnumerable<AvailabilityModel> GetAll()
        {
            return GetListIncluding(x => x.Id > 0);
        }

        public override bool DeleteById(int id)
        {            
            return base.DeleteById(id);
        }

        public override AvailabilityModel GetById(int id)
        {
            return GetSingleIncluding(x => x.Id == id);
        }
        public override bool Update(AvailabilityModel domainModel)
        {
            Availability dataModel = Repository.GetSingle<Availability>(x => x.Id == domainModel.Id);                

            dataModel.Created = domainModel.Created;          
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.StartDate = domainModel.StartDate;
            dataModel.EndDate = domainModel.EndDate;
            dataModel.Status = (AvailabilityStatus)domainModel.Status;
            


            Repository.Update<Availability>(dataModel);
            Repository.Commit();
            return true;
        }

        public override Availability ToDataModelWithChildNodes(AvailabilityModel domainModel, Availability dataModel = null)
        {
            if (dataModel == null)
                dataModel = new Availability();

            dataModel.Created = domainModel.Created;
            dataModel.Id = domainModel.Id;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.StartDate = domainModel.StartDate;
            dataModel.EndDate = domainModel.EndDate;
            dataModel.SlotId = domainModel.SlotId;            
            dataModel.Status = (AvailabilityStatus)domainModel.Status;
           

            return dataModel;
        }

        public override AvailabilityModel ToDomainModelWithChildNodes(Availability dataModel)
        {
            AvailabilityModel model = new AvailabilityModel();

            model.Id = dataModel.Id;
            model.StartDate = dataModel.StartDate;
            model.EndDate = dataModel.EndDate;
            model.CreatedBy = dataModel.CreatedBy;
            model.Created = dataModel.Created;
            model.ModifiedBy = dataModel.ModifiedBy;
            model.Modified = dataModel.Modified;
            model.SlotId = dataModel.SlotId;            
            model.Status = (ACP.Business.Enums.AvailabilityStatus)dataModel.Status;
            
            model.Slot = dataModel.Slot != null ? new SlotModel
            {
                Created = dataModel.Slot.Created,
                Id = dataModel.Slot.Id,
                CreatedBy = dataModel.Slot.CreatedBy,
                Modified = dataModel.Slot.Modified,
                ModifiedBy = dataModel.Slot.ModifiedBy,
                BookingEntityId = dataModel.Slot.BookingEntityId,
                IsOccupied = dataModel.Slot.IsOccupied,
                Identifier = dataModel.Slot.Identifier,
                BookingEntity = dataModel.Slot.BookingEntity != null ? new BookingEntityModel
                {
                    Name = dataModel.Slot.BookingEntity.Name,
                    Code = dataModel.Slot.BookingEntity.Code,
                    Id = dataModel.Slot.BookingEntity.Id,
                    RootBookingEntity = dataModel.Slot.BookingEntity.RootBookingEntity != null ? new RootBookingEntityModel
                    {
                        Name = dataModel.Slot.BookingEntity.RootBookingEntity.Name,
                        Code = dataModel.Slot.BookingEntity.RootBookingEntity.Code,
                        Id = dataModel.Slot.BookingEntity.RootBookingEntity.Id
                    } : null
                } : null
            } : null;

            return model;
            
        }

        public override Availability ToDataModel(AvailabilityModel domainModel, Availability dataModel = null)
        {
            if (dataModel==null)
                dataModel = new Availability();

            dataModel.Created = domainModel.Created;
            dataModel.Id = domainModel.Id;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.StartDate = domainModel.StartDate;
            dataModel.EndDate = domainModel.EndDate;
            dataModel.SlotId = domainModel.SlotId;
            dataModel.Status = (AvailabilityStatus)domainModel.Status;
            
            return dataModel;
        }

        public override AvailabilityModel ToDomainModel(Availability dataModel)
        {
            AvailabilityModel model = new AvailabilityModel();

            model.Id = dataModel.Id;
            model.StartDate = dataModel.StartDate;
            model.EndDate = dataModel.EndDate;           
            model.CreatedBy = dataModel.CreatedBy;
            model.Created = dataModel.Created;
            model.ModifiedBy = dataModel.ModifiedBy;
            model.Modified = dataModel.Modified;
            model.SlotId = dataModel.SlotId;
            model.Status = (ACP.Business.Enums.AvailabilityStatus)dataModel.Status;
            
            model.Slot = dataModel.Slot != null ? new SlotModel
            {
                Created = dataModel.Slot.Created,
                Id = dataModel.Slot.Id,
                CreatedBy = dataModel.Slot.CreatedBy,
                Modified = dataModel.Slot.Modified,
                ModifiedBy = dataModel.Slot.ModifiedBy,
                BookingEntityId = dataModel.Slot.BookingEntityId,
                IsOccupied = dataModel.Slot.IsOccupied,
                Identifier = dataModel.Slot.Identifier,
                BookingEntity = dataModel.Slot.BookingEntity != null ? new BookingEntityModel
                {
                    Name = dataModel.Slot.BookingEntity.Name,
                    Code = dataModel.Slot.BookingEntity.Code,
                    Id = dataModel.Slot.BookingEntity.Id,
                    RootBookingEntity = dataModel.Slot.BookingEntity.RootBookingEntity != null ? new RootBookingEntityModel
                    {
                        Name = dataModel.Slot.BookingEntity.RootBookingEntity.Name,
                        Code = dataModel.Slot.BookingEntity.RootBookingEntity.Code,
                        Id = dataModel.Slot.BookingEntity.RootBookingEntity.Id
                    } : null
                } : null
            } : null;
            return model;
            
        }
    }
}
