using ACP.Business;
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
    public class BookingServiceManager: BaseACPManager<BookingServiceModel, BookingService>, IBookingServiceManager
    {
        public BookingServiceManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public override BookingServiceModel Add(BookingServiceModel domainModel)
        {
            if (domainModel == null)
            {
                throw new ItemNotFoundException("Service model is not valid");
            }

            var dataService = ToDataModel(domainModel);

            dataService = Repository.Add<BookingService>(dataService);
            Repository.Commit();

            return ToDomainModel(dataService);
            //return base.Add(domainModel);
        }

        public override BookingService ToDataModel(BookingServiceModel domainModel, BookingService dataModel = null)
        {
            if (dataModel == null)
                dataModel = new BookingService();

            dataModel.Id = domainModel.Id;
            dataModel.Name = domainModel.Name;           
            dataModel.CreatedBy = domainModel.CreatedBy ?? dataModel.CreatedBy;
            dataModel.Created = domainModel.Created ?? dataModel.Created;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.Modified = domainModel.Modified;

            return dataModel;
        }

        public override BookingServiceModel ToDomainModel(BookingService service)
        {
            BookingServiceModel serviceModel = new BookingServiceModel();
            serviceModel.Id = service.Id;
            serviceModel.Name = service.Name;

            return serviceModel;
        }
    }
}
