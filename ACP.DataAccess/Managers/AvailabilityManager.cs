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
            return GetListIncluding(where, x => x.Status).ToList();
            //return GetListIncluding(x=>x.StartDate >=  && x.StartDate <= endDate3, x => x.Status, x).ToList();
        }

        public IList<AvailabilityModel> FindAvailabilityWithPrices(Func<Availability, bool> where)
        {
           

            return GetListIncluding(where, x => x.Status).ToList();
            //return GetListIncluding(x=>x.StartDate >=  && x.StartDate <= endDate3, x => x.Status, x).ToList();
        }       

        public IEnumerable<AvailabilityModel> GetAll()
        {
            return GetListIncluding(x => x.Id > 0, x => x.Status);
        }

        public override bool DeleteById(int id)
        {            
            return base.DeleteById(id);
        }

        public override AvailabilityModel GetById(int id)
        {
            return GetSingleIncluding(x => x.Id == id, y => y.Status);
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
            dataModel.SlotId = domainModel.SlotId;
            dataModel.StatusId = domainModel.StatusId;


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
            dataModel.StatusId = domainModel.StatusId;
            dataModel.Status = domainModel.Status != null ? new Status
            {
                Created = domainModel.Status.Created,
                Id = domainModel.Status.Id,
                CreatedBy = domainModel.Status.CreatedBy,
                Modified = domainModel.Status.Modified,
                ModifiedBy = domainModel.Status.ModifiedBy,
                StatusType = (Data.Enums.StatusType)domainModel.Status.StatusType

            } : null;

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
            model.StatusId = dataModel.Id;
            model.Status = dataModel.Status != null ? new StatusModel
            {                
                CreatedBy = dataModel.Status.CreatedBy,
                Created = dataModel.Status.Created,
                ModifiedBy = dataModel.Status.ModifiedBy,
                Modified = dataModel.Status.Modified,
               StatusType = (Business.Enums.StatusType)dataModel.Status.StatusType,
                Id = dataModel.Id
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
            dataModel.StatusId = domainModel.StatusId;
            dataModel.Status = domainModel.Status != null ? new Status
            {
                Created = domainModel.Status.Created,
                Id = domainModel.Status.Id,
                CreatedBy = domainModel.Status.CreatedBy,
                Modified = domainModel.Status.Modified,    
                ModifiedBy = domainModel.Status.ModifiedBy,
                 StatusType = (StatusType)domainModel.Status.StatusType
             
            } : null;

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
            model.StatusId = dataModel.StatusId;
            model.Status = dataModel.Status!=null?new StatusModel{
                CreatedBy = dataModel.Status.CreatedBy,
                Created = dataModel.Status.Created,
                ModifiedBy = dataModel.Status.ModifiedBy,
                Modified = dataModel.Status.Modified,
                 StatusType =(Business.Enums.StatusType) dataModel.Status.StatusType,
                  Id = dataModel.Id
            }:null;

            return model;
            
        }
    }
}
