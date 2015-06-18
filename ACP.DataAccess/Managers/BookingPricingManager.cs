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



        public bool AddPricesWithDays(int bookingEntityId, IList<BookingPricingModel> prices)
        {
            bool result = false;
            var bookingentity = Repository.GetSingle<BookingEntity>(x => x.Id == bookingEntityId);

            if (bookingentity != null)
            {
                foreach (var item in prices)
                {                    
                    item.BookingEntityId = bookingEntityId;
                    BookingPricing model = ToDataModelWithChildNodes(item);                   
                    Repository.Add<BookingPricing>(model);
                    Repository.Commit();
                    result = true;
                }
            }
            return result;
        }

        public override BookingPricing ToDataModelWithChildNodes(BookingPricingModel domainModel, BookingPricing dataModel = null)
        {
            BookingPricing model = new BookingPricing();


            model.Created = domainModel.Created;
            model.Id = domainModel.Id;
            model.CreatedBy = domainModel.CreatedBy;
            model.Modified = domainModel.Modified;
            model.ModifiedBy = domainModel.ModifiedBy;
            model.Name = domainModel.Name;
            model.Start = domainModel.Start;
            model.End = domainModel.End;
            model.BookingEntityId = domainModel.BookingEntityId;
           
            model.DayPrices = domainModel.DayPrices != null ? domainModel.DayPrices.Select(r => new DayPrice
            {
                Created = r.Created,
                CreatedBy = r.CreatedBy,
                Id = r.Id,
                Modified = r.Modified,
                ModifiedBy = r.ModifiedBy,
                Day= r.Day,
                Dayprice = r.Dayprice,
                HourPrices = r.HourPrices!=null?r.HourPrices.Select(m=>new HourPrice
                {
                    Created = m.Created,
                    CreatedBy = m.CreatedBy,                    
                    Modified = m.Modified,
                    ModifiedBy = m.ModifiedBy,
                    HourMinute = m.HourMinute,
                    Hourprice = m.Hourprice
                }).ToList():null

            }).ToList() : null;

            
            return model;

        }
    }
}
