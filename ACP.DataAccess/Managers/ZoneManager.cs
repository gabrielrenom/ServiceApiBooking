using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Data.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ACP.Data;

namespace ACP.DataAccess.Managers
{
    public class ZoneManager: BaseACPManager<ZoneModel, Zone>, IZoneManager
    {
        public ZoneManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public override ZoneModel Add(ZoneModel domainModel)
        {
            return base.Add(domainModel);
        }

        public override async Task<IEnumerable<ZoneModel>> GetAllAsync()
        {
            return await GetListIncludingAsync(x => x.Id > 0, x => x.Availability);
        }        

        public override bool DeleteById(int id)
        {
            return base.DeleteById(id);
        }

        public override bool Update(ZoneModel domainModel)
        {
            return base.Update(domainModel);    
        }

        public override Zone ToDataModel(ZoneModel domainModel, Zone dataModel = null)
        {
            if (dataModel == null)
                dataModel = new Zone();
       

            dataModel.Created = domainModel.Created;
            dataModel.Id = domainModel.Id;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.BookingEntityId = domainModel.BookingEntityId;
            dataModel.IsOccupied = domainModel.IsOccupied;
            dataModel.Identifier = domainModel.Identifier;
            dataModel.Availability = domainModel.Availability != null ? domainModel.Availability.Select(x => new Availability
            {
                Created = x.Created,
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                Modified = x.Modified,
                ModifiedBy = x.ModifiedBy,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                ZoneId = x.ZoneId,
                StatusId = x.StatusId,
                Status = x.Status != null ? new Status
                {
                    Created = x.Status.Created,
                    Id = x.Status.Id,
                    CreatedBy = x.Status.CreatedBy,
                    Modified = x.Status.Modified,
                    ModifiedBy = x.Status.ModifiedBy,
                    StatusType = (Data.Enums.StatusType)x.Status.StatusType
                } : null,
            }).ToList() : null;
           

            return dataModel;
        }

        public override ZoneModel ToDomainModel(Zone dataModel)
        {
            ZoneModel model = new ZoneModel
            {
                Created = dataModel.Created,
                Id = dataModel.Id,
                CreatedBy = dataModel.CreatedBy,
                Modified = dataModel.Modified,
                ModifiedBy = dataModel.ModifiedBy,
                BookingEntityId = dataModel.BookingEntityId,
                IsOccupied = dataModel.IsOccupied,
                Identifier = dataModel.Identifier,
                Availability = dataModel.Availability!=null? dataModel.Availability.Select(x=>new AvailabilityModel {
                     Created = x.Created,
                     Id = x.Id,
                     CreatedBy = x.CreatedBy,
                     Modified = x.Modified,
                     ModifiedBy = x.ModifiedBy,
                     StartDate = x.StartDate,
                     EndDate = x.EndDate,
                     ZoneId = x.ZoneId,
                     StatusId = x.StatusId,
                     Status = x.Status!=null?new StatusModel {
                             Created = x.Status.Created,
                             Id = x.Status.Id,
                             CreatedBy = x.Status.CreatedBy,
                             Modified = x.Status.Modified,
                             ModifiedBy = x.Status.ModifiedBy,
                             StatusType = (Business.Enums.StatusType)x.Status.StatusType
                         }:null,
                 }).ToList():null,                                  
            };

            return model;
        }

        public async Task<IList<ZoneModel>> GetAllFreeAsync()
        {
            var result = await GetListIncludingAsync(x => x.IsOccupied == false, x => x.Availability);
            return result.ToList();
        }

        public async Task<IList<ZoneModel>> GetAllOccupiedAsync()
        {
            var result = await GetListIncludingAsync(x => x.IsOccupied == true, x => x.Availability);
            return result.ToList();
        }

        public async Task<ZoneModel> GetByNumberIdentifierAsync(int? number=null, string identifier=null)
        {
            if (number != null && identifier == null)
                return await base.GetSingleAsync(x => x.Number == number);
            else if (number == null && identifier != null)
                return await base.GetSingleAsync(x => x.Identifier ==identifier);
            else if (number != null && identifier != null)
                return await base.GetSingleAsync(x => x.Number == number && x.Identifier == identifier);

            return null;

        }
    }
}
