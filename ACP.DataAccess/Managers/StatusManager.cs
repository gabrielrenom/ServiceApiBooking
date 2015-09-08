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
    public class StatusManager: BaseACPManager<StatusModel, Status>, IStatusManager
    {
        public StatusManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public StatusModel GetByName(string StatusName)
        {
            var dataModel = Repository.GetSingle<Status>(x => x.Name == StatusName);

            return ToDomainModel(dataModel);
        }

        public override StatusModel Add(StatusModel domainModel)
        {
            return base.Add(domainModel);
        }


        public override Status ToDataModel(StatusModel domainModel, Status dataModel = null)
        {
            if (dataModel == null)
                dataModel = new Status();

            dataModel.Created = domainModel.Created;
            dataModel.Id = domainModel.Id;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.Name = domainModel.Name;

            return dataModel;
        }

        public override StatusModel ToDomainModel(Status dataModel)
        {
            StatusModel model = new StatusModel
            {
                Created = dataModel.Created,
                Id = dataModel.Id,
                CreatedBy = dataModel.CreatedBy,
                Modified = dataModel.Modified,
                ModifiedBy = dataModel.ModifiedBy,
                Name = dataModel.Name        
            };

            return model;
        }
    }
}
