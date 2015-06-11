using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Business.Services;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Managers
{
    public class BookingPricingManager : BaseACPManager<BookingPricingModel, BookingPricing>, IBookingPricingManager
    {

        public BookingPricingManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }


        
        //IEnumerable<BookingPricingModel> IBaseManager<BookingPricingModel, BookingPricing>.GetAll()
        //{
        //    return GetList(x => x.Id > 0);
        //}

        public IList<BookingPricingModel> GetAllPrices(DateTime pickup, DateTime dropoff)
        {

            return GetListIncluding(x => pickup > x.Start && dropoff < x.End, x => x.BookingEntity)           
           .OrderBy(a => a.Name).ToList();
        }

        public override BookingPricingModel ToDomainModelWithChildNodes(BookingPricing dataModel)
        {
            BookingPricingModel domainModel = new BookingPricingModel();


            domainModel.Id = dataModel.Id;
            domainModel.Start = dataModel.Start;
            domainModel.End = dataModel.End;
            domainModel.Name = dataModel.Name;
            domainModel.CreatedBy = dataModel.CreatedBy ?? dataModel.CreatedBy;
            domainModel.Created = dataModel.Created ?? dataModel.Created;
            domainModel.ModifiedBy = dataModel.ModifiedBy;
            domainModel.Modified = dataModel.Modified;
            domainModel.BookingEntity = new BookingEntityModel
            {
                Id = dataModel.BookingEntity.Id,
                Comission = dataModel.BookingEntity.Comission,
                Name = dataModel.BookingEntity.Name,
                Price = dataModel.BookingEntity.Price,
                Sameday = dataModel.BookingEntity.Sameday
            };

            return domainModel;

        }

        public override BookingPricingModel ToDomainModel(BookingPricing dataModel)
        {
            BookingPricingModel domainModel = new BookingPricingModel();


            domainModel.Id = dataModel.Id;
            domainModel.Start = dataModel.Start;
            domainModel.End = dataModel.End;
            domainModel.Name = dataModel.Name;
            domainModel.CreatedBy = dataModel.CreatedBy ?? dataModel.CreatedBy;
            domainModel.Created = dataModel.Created ?? dataModel.Created;
            domainModel.ModifiedBy = dataModel.ModifiedBy;
            domainModel.Modified = dataModel.Modified;
            domainModel.BookingEntity = new BookingEntityModel
            {
                Id = dataModel.BookingEntity.Id,
                Comission = dataModel.BookingEntity.Comission,
                Name = dataModel.BookingEntity.Name,
                Price = dataModel.BookingEntity.Price,
                Sameday = dataModel.BookingEntity.Sameday
            };

            return domainModel;
            
        }
        
    }
}
