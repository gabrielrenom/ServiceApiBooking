using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using ACP.Business.Services;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Managers
{
    public class BookingPricingManager : BaseACPManager<BookingPricingModel, BookingPricing>, IBookingPricingManager
    {
        private IBookingEntityManager bookingEntityManager;


        public BookingPricingManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
            bookingEntityManager = new BookingEntityManager(repository);
        }

        public override async Task<BookingPricingModel> GetByIdAsync(int id)
        {
            return await base.GetByIdIncludingAsync(id, x => x.DayPrices);
        }

        //IEnumerable<BookingPricingModel> IBaseManager<BookingPricingModel, BookingPricing>.GetAll()
        //{
        //    return GetList(x => x.Id > 0);
        //}

        public IList<BookingPricingModel> GetAllPrices(DateTime pickup, DateTime dropoff)
        {

            return GetListIncluding(x => pickup > x.Start && dropoff < x.End,
                x => x.BookingEntity,
                x => x.BookingEntity.Prices,
                x => x.BookingEntity.Prices.Select(y => x.DayPrices))
                .OrderBy(a => a.Name).ToList();

        }

        public IList<BookingPricingModel> GetAllPricesByBookEntity(int bookingentityid, DateTime pickup, DateTime dropoff)
        {

            //return GetListIncluding(x=>x.BookingEntityId == bookingentityid && pickup > x.Start && dropoff < x.End,
            return GetListIncluding(x => x.BookingEntityId == bookingentityid && dropoff > x.Start && pickup < x.End,
                x => x.BookingEntity,
                x => x.BookingEntity.Prices,
                x => x.BookingEntity.Prices.Select(y => x.DayPrices))
                .OrderBy(a => a.Name).ToList();

        }

        public IList<BookingPricingModel> GetAllPricesByPickLocationAndDropLocation(string pickuplocation, string droplocation, DateTime pickup, DateTime dropoff)
        {

            return GetListIncluding(x => x.BookingEntity.Name.ToLower().Contains(droplocation.ToLower()) == true && pickup > x.Start && dropoff < x.End,
                x => x.BookingEntity,
                x => x.BookingEntity.Prices,
                x => x.BookingEntity.Prices.Select(y => x.DayPrices))
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
            domainModel.BookingEntityId = dataModel.BookingEntityId;
            domainModel.BookingEntity = dataModel.BookingEntity != null ? new BookingEntityModel
            {
                Id = dataModel.BookingEntity.Id,
                Comission = dataModel.BookingEntity.Comission,
                Name = dataModel.BookingEntity.Name,
                Price = dataModel.BookingEntity.Price,
                Sameday = dataModel.BookingEntity.Sameday
            } : null;

            domainModel.DayPrices = dataModel.DayPrices != null ? dataModel.DayPrices.Select(r => new DayPriceModel
            {
                Created = r.Created,
                CreatedBy = r.CreatedBy,
                Id = r.Id,
                Modified = r.Modified,
                ModifiedBy = r.ModifiedBy,
                Day = r.Day,
                Dayprice = r.Dayprice,
                BookingPricingId = r.BookingPricingId,
                HourPrices = r.HourPrices != null ? r.HourPrices.Select(m => new HourPriceModel
                {
                    Created = m.Created,
                    CreatedBy = m.CreatedBy,
                    Modified = m.Modified,
                    ModifiedBy = m.ModifiedBy,
                    HourMinute = m.HourMinute,
                    Hourprice = m.Hourprice,
                    Id = m.Id,
                    DayPriceId = m.DayPriceId
                }).ToList() : null

            }).ToList() : null;

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
            domainModel.BookingEntity = dataModel.BookingEntity != null ? new BookingEntityModel
            {
                Id = dataModel.BookingEntity.Id,
                Comission = dataModel.BookingEntity.Comission,
                Name = dataModel.BookingEntity.Name,
                Price = dataModel.BookingEntity.Price,
                Sameday = dataModel.BookingEntity.Sameday
            } : null;

            domainModel.DayPrices = dataModel.DayPrices != null ? dataModel.DayPrices.Select(r => new DayPriceModel
            {
                Created = r.Created,
                CreatedBy = r.CreatedBy,
                Id = r.Id,
                Modified = r.Modified,
                ModifiedBy = r.ModifiedBy,
                Day = r.Day,
                Dayprice = r.Dayprice,
                BookingPricingId = r.BookingPricingId,
                HourPrices = r.HourPrices != null ? r.HourPrices.Select(m => new HourPriceModel
                {
                    Created = m.Created,
                    CreatedBy = m.CreatedBy,
                    Modified = m.Modified,
                    ModifiedBy = m.ModifiedBy,
                    HourMinute = m.HourMinute,
                    Hourprice = m.Hourprice,
                    Id = m.Id,
                    DayPriceId = m.DayPriceId
                }).ToList() : null

            }).ToList() : null;

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
                Day = r.Day,
                Dayprice = r.Dayprice,
                HourPrices = r.HourPrices != null ? r.HourPrices.Select(m => new HourPrice
                {
                    Created = m.Created,
                    CreatedBy = m.CreatedBy,
                    Modified = m.Modified,
                    ModifiedBy = m.ModifiedBy,
                    HourMinute = m.HourMinute,
                    Hourprice = m.Hourprice,
                    DayPriceId = m.DayPriceId,
                    Id = m.Id

                }).ToList() : null,
                BookingPricingId = r.BookingPricingId

            }).ToList() : null;


            return model;

        }


        public IList<BookingPricingModel> GetAllPricesWithDays(int bookingEntityId)
        {
            return GetListIncluding(x => x.BookingEntityId == bookingEntityId, x => x.DayPrices.Select(y => y.HourPrices)).ToList();
        }

        public IList<BookingPricingModel> GetAllPricesWithDaysAndTimes(int bookingEntityId)
        {
            return GetListIncluding(x => x.BookingEntityId == bookingEntityId, x => x.DayPrices, x => x.DayPrices.Select(y => y.HourPrices)).ToList();
        }

        public override async Task<bool> UpdateAsync(BookingPricingModel domainModel)
        {
            bool result = false;

            var model = Repository.GetSingle<BookingPricing>(x => x.Id == domainModel.Id,
                         y => y.BookingEntity, y => y.DayPrices, x => x.DayPrices.Select(y => y.HourPrices));

            model.Created = domainModel.Created;
            model.CreatedBy = domainModel.CreatedBy;
            model.Modified = domainModel.Modified;
            model.ModifiedBy = domainModel.ModifiedBy;
            model.Name = domainModel.Name;
            model.Start = domainModel.Start;
            model.End = domainModel.End;

            if (model.DayPrices.Count > 0)
            {
                Repository.DeleteMany<DayPrice>(model.DayPrices.ToArray());

                model.DayPrices = domainModel.DayPrices != null ? domainModel.DayPrices.Select(r => new DayPrice
                {
                    Created = r.Created,
                    CreatedBy = r.CreatedBy,
                    Modified = r.Modified,
                    ModifiedBy = r.ModifiedBy,
                    Day = r.Day,
                    Dayprice = r.Dayprice,
                    HourPrices = r.HourPrices != null ? r.HourPrices.Select(m => new HourPrice
                    {
                        Created = m.Created,
                        CreatedBy = m.CreatedBy,
                        Modified = m.Modified,
                        ModifiedBy = m.ModifiedBy,
                        HourMinute = m.HourMinute,
                        Hourprice = m.Hourprice,
                        DayPriceId = m.DayPriceId,

                    }).ToList() : null,

                }).ToList() : null;

            }

            Repository.Update<BookingPricing>(model);
            await Repository.CommitAsync();
            result = true;
        
            return result;
        }

        public bool UpdatePricesWithDays(int bookingEntityId, IList<BookingPricingModel> prices)
        {
            bool result = false;
            var bookingentity = Repository.GetSingle<BookingEntity>(x => x.Id == bookingEntityId );

            if (bookingentity != null)
            {
                foreach (var domainModel in prices)
                {                    
                    var model = Repository.GetSingle<BookingPricing>(x => x.Id == domainModel.Id,
                        y => y.BookingEntity, y => y.DayPrices, x => x.DayPrices.Select(y => y.HourPrices));

                    model.Created = domainModel.Created;                    
                    model.CreatedBy = domainModel.CreatedBy;
                    model.Modified = domainModel.Modified;
                    model.ModifiedBy = domainModel.ModifiedBy;
                    model.Name = domainModel.Name;
                    model.Start = domainModel.Start;
                    model.End = domainModel.End;                    

                    if (model.DayPrices.Count > 0)
                    {
                        Repository.DeleteMany<DayPrice>(model.DayPrices.ToArray());

                        model.DayPrices = domainModel.DayPrices != null ? domainModel.DayPrices.Select(r => new DayPrice
                        {
                            Created = r.Created,
                            CreatedBy = r.CreatedBy,                            
                            Modified = r.Modified,
                            ModifiedBy = r.ModifiedBy,
                            Day = r.Day,
                            Dayprice = r.Dayprice,
                            HourPrices = r.HourPrices != null ? r.HourPrices.Select(m => new HourPrice
                            {
                                Created = m.Created,
                                CreatedBy = m.CreatedBy,
                                Modified = m.Modified,
                                ModifiedBy = m.ModifiedBy,
                                HourMinute = m.HourMinute,
                                Hourprice = m.Hourprice,
                                DayPriceId = m.DayPriceId,                                

                            }).ToList() : null,
                            
                        }).ToList() : null;

                    }                   

                    Repository.Update<BookingPricing>(model);
                    Repository.Commit();
                    result = true;
                }
            }
            return result;

        }

        public override bool DeleteById(int id)
        {
            var model = Repository.GetSingle<BookingPricing>(a => a.Id == id, x => x.BookingEntity, x => x.DayPrices, x => x.DayPrices.Select(y => y.HourPrices));


            if (model.DayPrices!=null)
            {
                Repository.DeleteMany<DayPrice>(model.DayPrices.ToArray());
            }

            Repository.Delete<BookingPricing>(model);
            Repository.Commit();

            return true;

        }

        public bool AddPricesWithDaysAndTime(int bookingEntityId, IList<BookingPricingModel> prices)
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
    }
}
