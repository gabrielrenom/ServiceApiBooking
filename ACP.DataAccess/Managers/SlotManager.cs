using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using ACP.Data;

namespace ACP.DataAccess.Managers
{
    public class SlotManager: BaseACPManager<SlotModel, Slot>, ISlotManager
    {
        public SlotManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public override SlotModel Add(SlotModel domainModel)
        {
            return base.Add(domainModel);
        }

        public override async Task<IEnumerable<SlotModel>> GetAllAsync()
        {
            return await GetListIncludingAsync(x => x.Id > 0, x => x.Availability);
        }        

        public override bool DeleteById(int id)
        {
            return base.DeleteById(id);
        }

        public async Task<SlotModel> GetWithAllAvailabilitiesById(int id)
        {
            return await base.GetByIdIncludingAsync(id, x => x.Availability);  
        }

        public override bool Update(SlotModel domainModel)
        {
            bool result = false;
            var bookingentity = Repository.GetSingle<BookingEntity>(x => x.Id == domainModel.BookingEntityId);

            if (bookingentity != null)
            {                
                var model = Repository.GetSingle<Slot>(x => x.Id == domainModel.Id, x => x.Availability);
                model.Created = domainModel.Created;
                model.CreatedBy = domainModel.CreatedBy;
                model.Modified = domainModel.Modified;
                model.ModifiedBy = domainModel.ModifiedBy;
                model.Id = domainModel.Id;
                model.Number = domainModel.Number;
                model.Identifier = domainModel.Identifier;
                model.IsOccupied = domainModel.IsOccupied;

                if (model.Availability.Count > 0)
                    {
                        Repository.DeleteMany<Availability>(model.Availability.ToArray());

                        model.Availability = domainModel.Availability != null ? domainModel.Availability.Select(r => new Availability
                        {
                            Created = r.Created,
                            CreatedBy = r.CreatedBy,
                            Modified = r.Modified,
                            ModifiedBy = r.ModifiedBy,
                            EndDate = r.EndDate,
                            StartDate = r.StartDate,
                            Status = (ACP.Data.Enums.AvailabilityStatus)r.Status,
                            SlotId = r.SlotId,
                            Id = r.Id,
                           }).ToList() : null;
                    }

                    Repository.Update<Slot>(model);
                    Repository.Commit();
                    result = true;                
            }
            return result;
        }

        public async Task<IList<SlotModel>> FindSlotAvailable(DateTime startdate, DateTime enddate, string rootname)
        {

            return GetListIncluding(x => x.IsOccupied == false,
                x => x.Availability,
                x => x.BookingEntity,
                x => x.BookingEntity.RootBookingEntity)
                .Where(x => x.BookingEntity.RootBookingEntity.Name.Contains(rootname))
                .Where(x => x.Availability
                .Where(y => y.StartDate >= startdate &&
                       y.StartDate <= enddate)
                .Count() == 0)
                .ToList();         
        }

        public async Task<IList<SlotModel>> FindSlotAvailableByBookingEntityCode(DateTime startdate, DateTime enddate, string code)
        {
            //##REVISE
            return GetListIncluding(x => x.IsOccupied == false,
                x => x.Availability,
                x => x.BookingEntity,
                x => x.BookingEntity.RootBookingEntity)
                .Where(x => x.BookingEntity.Code.Contains(code))
                .Where(x => x.Availability
                .Where(y => y.StartDate >= startdate &&
                       y.StartDate <= enddate)
                .Count() == 0)
                .ToList();
        }

        public override Slot ToDataModel(SlotModel domainModel, Slot dataModel = null)
        {
            if (dataModel == null)
                dataModel = new Slot();
       

            dataModel.Created = domainModel.Created;
            dataModel.Id = domainModel.Id;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.BookingEntityId = domainModel.BookingEntityId;
            dataModel.IsOccupied = domainModel.IsOccupied;
            dataModel.Identifier = domainModel.Identifier;
            dataModel.Number = domainModel.Number;
            dataModel.Availability = domainModel.Availability != null ? domainModel.Availability.Select(x => new Availability
            {
                Created = x.Created,
                Id = x.Id,
                CreatedBy = x.CreatedBy,
                Modified = x.Modified,
                ModifiedBy = x.ModifiedBy,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                SlotId = x.SlotId,
                Status = (ACP.Data.Enums.AvailabilityStatus)x.Status,
                
            }).ToList() : null;
           

            return dataModel;
        }

        public override SlotModel ToDomainModel(Slot dataModel)
        {
            SlotModel model = new SlotModel
            {
                Created = dataModel.Created,
                Id = dataModel.Id,
                CreatedBy = dataModel.CreatedBy,
                Modified = dataModel.Modified,
                ModifiedBy = dataModel.ModifiedBy,
                BookingEntityId = dataModel.BookingEntityId,
                IsOccupied = dataModel.IsOccupied,
                Identifier = dataModel.Identifier,
                Number = dataModel.Number,
                BookingEntity = dataModel.BookingEntity!=null?
                new BookingEntityModel {
                    Code = dataModel.BookingEntity.Code,
                    Comission = dataModel.BookingEntity.Comission,
                    Created = dataModel.BookingEntity.Created,
                    Id = dataModel.BookingEntity.Id,
                    CreatedBy = dataModel.BookingEntity.CreatedBy,
                    Image = dataModel.BookingEntity.Image,
                    Logo = dataModel.BookingEntity.Logo,
                    Modified = dataModel.BookingEntity.Modified,
                    ModifiedBy = dataModel.BookingEntity.ModifiedBy,
                    Name = dataModel.BookingEntity.Name,
                    Price = dataModel.BookingEntity.Price,
                    Sameday = dataModel.BookingEntity.Sameday,
                    Properties = dataModel.BookingEntity.Properties != null ? dataModel.BookingEntity.Properties.Select(x => new PropertyModel
                    {
                        Id = x.Id,
                        Created = x.Created,
                        CreatedBy = x.CreatedBy,
                        Modified = x.Modified,
                        ModifiedBy = x.ModifiedBy,
                        Type = (PropertyType)x.Type,
                        BookingEntityId = x.BookingEntityId,
                        Key = x.Key,
                        Value = x.Value
                    }).ToList() : null,
                    Address = dataModel.BookingEntity.Address!=null?new AddressModel
                    {

                        Address1 = dataModel.BookingEntity.Address.Address1,
                        Address2 = dataModel.BookingEntity.Address.Address2,
                        Country = dataModel.BookingEntity.Address.Country,
                        County = dataModel.BookingEntity.Address.County,
                        Created = dataModel.BookingEntity.Address.Created,
                        CreatedBy = dataModel.BookingEntity.Address.CreatedBy,
                        Id = dataModel.BookingEntity.Address.Id,
                        Modified = dataModel.BookingEntity.Address.Modified,
                        ModifiedBy = dataModel.BookingEntity.Address.ModifiedBy,
                        Number = dataModel.BookingEntity.Address.Number,
                        Postcode = dataModel.BookingEntity.Address.Postcode
                    }:null,
                    RootBookingEntity = dataModel.BookingEntity.RootBookingEntity!=null?
                    new RootBookingEntityModel
                    {
                        Code = dataModel.BookingEntity.RootBookingEntity.Code,
                        Created = dataModel.BookingEntity.RootBookingEntity.Created,
                        Id = dataModel.BookingEntity.RootBookingEntity.Id,
                        CreatedBy = dataModel.BookingEntity.RootBookingEntity.CreatedBy,
                        Modified = dataModel.BookingEntity.RootBookingEntity.Modified,
                        ModifiedBy = dataModel.BookingEntity.RootBookingEntity.ModifiedBy,
                        Name = dataModel.BookingEntity.RootBookingEntity.Name,
                        Address = dataModel.BookingEntity.RootBookingEntity.Address!=null? new AddressModel
                        {

                            Address1 = dataModel.BookingEntity.RootBookingEntity.Address.Address1,
                            Address2 = dataModel.BookingEntity.RootBookingEntity.Address.Address2,
                            Country = dataModel.BookingEntity.RootBookingEntity.Address.Country,
                            County = dataModel.BookingEntity.RootBookingEntity.Address.County,
                            Created = dataModel.BookingEntity.RootBookingEntity.Address.Created,
                            CreatedBy = dataModel.BookingEntity.RootBookingEntity.Address.CreatedBy,
                            Id = dataModel.BookingEntity.RootBookingEntity.Address.Id,
                            Modified = dataModel.BookingEntity.RootBookingEntity.Address.Modified,
                            ModifiedBy = dataModel.BookingEntity.RootBookingEntity.Address.ModifiedBy,
                            Number = dataModel.BookingEntity.RootBookingEntity.Address.Number,
                            Postcode = dataModel.BookingEntity.RootBookingEntity.Address.Postcode
                        }:null,
                        AddressId = dataModel.BookingEntity.RootBookingEntity.AddressId,
                        StatusId = dataModel.BookingEntity.RootBookingEntity.StatusId,
                        Telephone = dataModel.BookingEntity.RootBookingEntity.Telephone,
                        Status = dataModel.BookingEntity.RootBookingEntity.Status!=null?new StatusModel
                        {
                            Created = dataModel.BookingEntity.RootBookingEntity.Status.Created,
                            CreatedBy = dataModel.BookingEntity.RootBookingEntity.Status.CreatedBy,
                            Id = dataModel.BookingEntity.RootBookingEntity.Status.Id,
                            Modified = dataModel.BookingEntity.RootBookingEntity.Status.Modified,
                            ModifiedBy = dataModel.BookingEntity.RootBookingEntity.Status.ModifiedBy,
                            StatusType = (Business.Enums.StatusType)dataModel.BookingEntity.RootBookingEntity.Status.StatusType
                        }:null,
                        Properties = dataModel.BookingEntity.RootBookingEntity.Properties != null ? dataModel.BookingEntity.RootBookingEntity.Properties.Select(x => new RootBookingPropertyModel
                        {
                            Id = x.Id,
                            Created = x.Created,
                            CreatedBy = x.CreatedBy,
                            Modified = x.Modified,
                            ModifiedBy = x.ModifiedBy,
                            PropertyType = (Business.Enums.RootBookingPropertyType)x.Type,
                            RootBookingEntityId = x.RootBookingEntityId,
                            Key = x.Key,
                            Value = x.Value
                        }).ToList() : null,                       
                    }
                    : null,
                    Extras = dataModel.BookingEntity.Extras != null ? dataModel.BookingEntity.Extras.Select(x => new ExtraModel
                    {
                        Created = x.Created,
                        CreatedBy = x.CreatedBy,
                        Id = x.Id,
                        Modified = x.Modified,
                        ModifiedBy = x.ModifiedBy,
                        BookingEntityId = x.BookingEntityId,
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description
                    }).ToList() : null,
                    Prices = dataModel.BookingEntity.Prices != null ? dataModel.BookingEntity.Prices.Select(x => new BookingPricingModel
                    {
                        Created = x.Created,
                        CreatedBy = x.CreatedBy,
                        Id = x.Id,
                        Modified = x.Modified,
                        ModifiedBy = x.ModifiedBy,
                        BookingEntityId = x.BookingEntityId,
                        End = x.End,
                        Name = x.Name,
                        Start = x.Start,
                        DayPrices = x.DayPrices != null ? x.DayPrices.Select(y => new DayPriceModel
                        {
                            Created = y.Created,
                            CreatedBy = y.CreatedBy,
                            Id = y.Id,
                            Modified = y.Modified,
                            ModifiedBy = y.ModifiedBy,
                            BookingPricingId = y.BookingPricingId,
                            Day = y.Day,
                            Dayprice = y.Dayprice,
                            HourPrices = y.HourPrices != null ? y.HourPrices.Select(u => new HourPriceModel
                            {
                                Created = u.Created,
                                CreatedBy = u.CreatedBy,
                                Id = u.Id,
                                Modified = u.Modified,
                                ModifiedBy = u.ModifiedBy,
                                DayPriceId = u.DayPriceId,
                                HourMinute = u.HourMinute,
                                Hourprice = u.Hourprice
                            }).ToList() : null
                        }).ToList() : null
                    }).ToList() : null
                } : null,                
                Availability = dataModel.Availability!=null? dataModel.Availability.Select(x=>new AvailabilityModel {
                     Created = x.Created,
                     Id = x.Id,
                     CreatedBy = x.CreatedBy,
                     Modified = x.Modified,
                     ModifiedBy = x.ModifiedBy,
                     StartDate = x.StartDate,
                     EndDate = x.EndDate,
                     SlotId = x.SlotId,
                     Status = (ACP.Business.Enums.AvailabilityStatus)x.Status
                     
                 }).ToList():null,                                  
            };

            return model;
        }

        public async Task<IList<SlotModel>> GetAllFreeAsync()
        {
            var result = await GetListIncludingAsync(x => x.IsOccupied == false, x => x.Availability);
            return result.ToList();
        }

        public async Task<IList<SlotModel>> GetAllFreeAsync(string bookingentitycode)
        {
            var result = await GetListIncludingAsync(x => x.IsOccupied == false && x.BookingEntity.Code.ToLower().Contains(bookingentitycode.ToLower()), x => x.Availability, x=>x.BookingEntity);
            return result.ToList();
        }

        public async Task<IList<SlotModel>> GetAllOccupiedAsync()
        {
            var result = await GetListIncludingAsync(x => x.IsOccupied == true, x => x.Availability);
            return result.ToList();
        }

        public async Task<SlotModel> GetByNumberIdentifierAsync(int? number=null, string identifier=null)
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
