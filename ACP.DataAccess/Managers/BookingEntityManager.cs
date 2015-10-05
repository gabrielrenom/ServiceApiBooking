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
    public class BookingEntityManager : BaseACPManager<BookingEntityModel, BookingEntity>, IBookingEntityManager
    {
        public BookingEntityManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public override BookingEntityModel ToDomainModel(BookingEntity dataModel)
        {
            BookingEntityModel model = new BookingEntityModel
            {
                Comission = dataModel.Comission,
                 Code = dataModel.Code,
                Created = dataModel.Created,
                Id = dataModel.Id,
                CreatedBy = dataModel.CreatedBy,
                Image = dataModel.Image,
                Logo = dataModel.Logo,
                Modified = dataModel.Modified,
                ModifiedBy = dataModel.ModifiedBy,
                Name = dataModel.Name,
                Price = dataModel.Price,
                Sameday = dataModel.Sameday,
                Properties = dataModel.Properties != null ? dataModel.Properties.Select(x => new PropertyModel
                 {
                     Id = x.Id,
                     Created = x.Created,
                     CreatedBy = x.CreatedBy,
                     Modified = x.Modified,
                     ModifiedBy = x.ModifiedBy,                     
                     Type = (PropertyType) x.Type,
                     BookingEntityId = x.BookingEntityId,
                     Key = x.Key,
                     Value = x.Value
                 }).ToList() : null,
                Address =dataModel.Address!=null?new AddressModel
                {

                    Address1 = dataModel.Address.Address1,
                    Address2 = dataModel.Address.Address2,
                    Country = dataModel.Address.Country,
                    County = dataModel.Address.County,
                    Created = dataModel.Address.Created,
                    CreatedBy = dataModel.Address.CreatedBy,
                    Id = dataModel.Address.Id,
                    Modified = dataModel.Address.Modified,
                    ModifiedBy = dataModel.Address.ModifiedBy,
                    Number = dataModel.Address.Number,
                    Postcode = dataModel.Address.Postcode,
                    City = dataModel.Address.City
                }:null,
                Extras = dataModel.Extras != null ? dataModel.Extras.Select(x => new ExtraModel
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
                Prices = dataModel.Prices != null ? dataModel.Prices.Select(x => new BookingPricingModel
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
            };

            if (dataModel.RootBookingEntity != null)
            {
                model.RootBookingEntity = ToDomainModelRootBookingModel(dataModel.RootBookingEntity);
            }

            return model;
        }

        public RootBookingEntityModel ToDomainModelRootBookingModel(RootBookingEntity dataModel)
        {
            RootBookingEntityModel model = new RootBookingEntityModel
            {
                Created = dataModel.Created,
                Id = dataModel.Id,
                CreatedBy = dataModel.CreatedBy,
                Modified = dataModel.Modified,
                ModifiedBy = dataModel.ModifiedBy,
                Name = dataModel.Name,
                 Code = dataModel.Code,
                  Website = dataModel.Website,
                Address = dataModel.Address!=null?new AddressModel
                {

                    Address1 = dataModel.Address.Address1,
                    Address2 = dataModel.Address.Address2,
                    Country = dataModel.Address.Country,
                    County = dataModel.Address.County,
                    Created = dataModel.Address.Created,
                    CreatedBy = dataModel.Address.CreatedBy,
                    Id = dataModel.Address.Id,
                    Modified = dataModel.Address.Modified,
                    ModifiedBy = dataModel.Address.ModifiedBy,
                    Number = dataModel.Address.Number,
                    Postcode = dataModel.Address.Postcode
                }:null,
                AddressId = dataModel.AddressId,
                StatusId = dataModel.StatusId,
                Telephone = dataModel.Telephone,
                Status = dataModel.Status!=null?new StatusModel
                {
                    Created = dataModel.Status.Created,
                    CreatedBy = dataModel.Status.CreatedBy,
                    Id = dataModel.Status.Id,
                    Modified = dataModel.Status.Modified,
                    ModifiedBy = dataModel.Status.ModifiedBy,
                     StatusType = (Business.Enums.StatusType)dataModel.Status.StatusType
                }:null,
                Properties = dataModel.Properties != null ? dataModel.Properties.Select(x => new RootBookingPropertyModel
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
                }).ToList() : null               
            };

            return model;
        }


        public override BookingEntityModel ToDomainModelWithChildNodes(BookingEntity dataModel)
        {
            BookingEntityModel model = new BookingEntityModel
            {
                 Code = dataModel.Code,
                Comission = dataModel.Comission,
                Created = dataModel.Created,
                Id = dataModel.Id,
                CreatedBy = dataModel.CreatedBy,
                Image = dataModel.Image,
                Logo = dataModel.Logo,
                Modified = dataModel.Modified,
                ModifiedBy = dataModel.ModifiedBy,
                Name = dataModel.Name,
                Price = dataModel.Price,
                Sameday = dataModel.Sameday,
                Properties = dataModel.Properties != null ? dataModel.Properties.Select(x => new PropertyModel
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
                Address = dataModel.Address!=null?new AddressModel
                {
                    Address1 = dataModel.Address.Address1,
                    Address2 = dataModel.Address.Address2,
                    Country = dataModel.Address.Country,
                    County = dataModel.Address.County,
                    Created = dataModel.Address.Created,
                    CreatedBy = dataModel.Address.CreatedBy,
                    Id = dataModel.Address.Id,
                    Modified = dataModel.Address.Modified,
                    ModifiedBy = dataModel.Address.ModifiedBy,
                    Number = dataModel.Address.Number,
                    Postcode = dataModel.Address.Postcode
                }:null,
                Extras = dataModel.Extras != null ? dataModel.Extras.Select(x => new ExtraModel
                {
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    Id = x.Id,
                    Modified = x.Modified,
                    ModifiedBy = x.ModifiedBy,
                    BookingEntityId = x.BookingEntityId,
                    Name= x.Name,
                    Price=x.Price,
                    Description=x.Description                        
                }).ToList() : null,
                Prices = dataModel.Prices != null ? dataModel.Prices.Select(x => new BookingPricingModel {
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    Id = x.Id,
                    Modified = x.Modified,
                    ModifiedBy = x.ModifiedBy,
                    BookingEntityId = x.BookingEntityId,
                    End=x.End,
                    Name = x.Name,
                    Start=x.Start,
                    DayPrices = x.DayPrices!=null?x.DayPrices.Select(y=>new DayPriceModel{
                        Created = y.Created,
                        CreatedBy = y.CreatedBy,
                        Id = y.Id,
                        Modified = y.Modified,
                        ModifiedBy = y.ModifiedBy,
                        BookingPricingId=y.BookingPricingId,
                        Day=y.Day,
                        Dayprice= y.Dayprice,
                        HourPrices = y.HourPrices!=null?y.HourPrices.Select(u=>new HourPriceModel{
                            Created = u.Created,
                            CreatedBy = u.CreatedBy,
                            Id = u.Id,
                            Modified = u.Modified,
                            ModifiedBy = u.ModifiedBy,
                            DayPriceId =u.DayPriceId,
                            HourMinute= u.HourMinute,
                            Hourprice = u.Hourprice
                            }).ToList():null
                        }).ToList():null
                }).ToList() : null

            };

            return model;
        }

        public override BookingEntity ToDataModel(BookingEntityModel domainModel, BookingEntity dataModel = null)
        {
            if (dataModel == null)
                dataModel = new BookingEntity();

                dataModel.Code = domainModel.Code;
                dataModel.Comission = domainModel.Comission;
                dataModel.Created = domainModel.Created;
                dataModel.Id = domainModel.Id;
                dataModel.CreatedBy = domainModel.CreatedBy;
                dataModel.Image = domainModel.Image;
                dataModel.Logo = domainModel.Logo;
                dataModel.Modified = domainModel.Modified;
                dataModel.ModifiedBy = domainModel.ModifiedBy;
                dataModel.Name = domainModel.Name;
                dataModel.Price = domainModel.Price;
                dataModel.Sameday = domainModel.Sameday;
                dataModel.RootBookingEntityId = domainModel.RootBookEntityId;
                dataModel.Properties = dataModel.Properties != null ? dataModel.Properties.Select(x => new Property
                {
                    Id = x.Id,
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    Modified = x.Modified,
                    ModifiedBy = x.ModifiedBy,
                    Type = (Data.Enums.PropertyType)x.Type,
                    BookingEntityId = x.BookingEntityId,
                    Key = x.Key,
                    Value = x.Value
                }).ToList() : null;
                dataModel.Address = new Address
                {
                    Address1 = domainModel.Address.Address1,
                    Address2 = domainModel.Address.Address2,
                    Country = domainModel.Address.Country,
                    County = domainModel.Address.County,
                    Created = domainModel.Address.Created,
                    CreatedBy = domainModel.Address.CreatedBy,
                    Id = domainModel.Address.Id,
                    Modified = domainModel.Address.Modified,
                    ModifiedBy = domainModel.Address.ModifiedBy,
                    Number = domainModel.Address.Number,
                    Postcode = domainModel.Address.Postcode
                };
                dataModel.Extras = domainModel.Extras != null ? domainModel.Extras.Select(x => new Extra
                {
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    Id = x.Id,
                    Modified = x.Modified,
                    ModifiedBy = x.ModifiedBy,
                    BookingEntityId = x.BookingEntityId,
                    Name= x.Name,
                    Price=x.Price,
                    Description=x.Description                        
                }).ToList() : null;
                dataModel.Prices = domainModel.Prices != null ? domainModel.Prices.Select(x => new BookingPricing {
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    Id = x.Id,
                    Modified = x.Modified,
                    ModifiedBy = x.ModifiedBy,
                    BookingEntityId = x.BookingEntityId,
                    End=x.End,
                    Name = x.Name,
                    Start=x.Start,
                    DayPrices = x.DayPrices!=null?x.DayPrices.Select(y=>new DayPrice{
                        Created = y.Created,
                        CreatedBy = y.CreatedBy,
                        Id = y.Id,
                        Modified = y.Modified,
                        ModifiedBy = y.ModifiedBy,
                        BookingPricingId=y.BookingPricingId,
                        Day=y.Day,
                        Dayprice= y.Dayprice,
                        HourPrices = y.HourPrices!=null?y.HourPrices.Select(u=>new HourPrice{
                            Created = u.Created,
                            CreatedBy = u.CreatedBy,
                            Id = u.Id,
                            Modified = u.Modified,
                            ModifiedBy = u.ModifiedBy,
                            DayPriceId =u.DayPriceId,
                            HourMinute= u.HourMinute,
                            Hourprice = u.Hourprice
                            }).ToList():null
                        }).ToList():null
                }).ToList() : null;

            return dataModel;
        }

        public IList<BookingEntityModel> GetAllBookingEntities()
        {
            return GetListIncluding(x => x.Id > 0, x => x.Address, x => x.Extras, x => x.Properties, x => x.Prices.Select(e => e.DayPrices), x => x.Prices.Select(w => w.DayPrices.Select(y => y.HourPrices))).ToList();                        
        }

        public override BookingEntityModel GetById(int id)
        {
            return GetByIdIncluding(id, x => x.Address, x=>x.Extras, x => x.Properties, x => x.Prices.Select(e=>e.DayPrices), x=>x.Prices.Select(w=>w.DayPrices.Select(y=>y.HourPrices)));
        }

        public override BookingEntityModel Add(BookingEntityModel domainModel)
        {           
            return base.Add(domainModel);
        }

        public override bool Update(BookingEntityModel domainModel)
        {
            var record = Repository.GetSingle<BookingEntity>(x=>x.Id==domainModel.Id,x => x.Address, x=>x.Extras, x => x.Properties,x => x.Prices.Select(e=>e.DayPrices), x=>x.Prices.Select(w=>w.DayPrices.Select(y=>y.HourPrices) ));

                record.Code = domainModel.Code;
                record.Comission = domainModel.Comission;
                record.Created = domainModel.Created;
                record.Id = domainModel.Id;
                record.CreatedBy = domainModel.CreatedBy;
                record.Image = domainModel.Image;
                record.Logo = domainModel.Logo;
                record.Modified = domainModel.Modified;
                record.ModifiedBy = domainModel.ModifiedBy;
                record.Name = domainModel.Name;
                record.Price = domainModel.Price;
                record.Sameday = domainModel.Sameday;
                record.Properties = record.Properties != null ? record.Properties.Select(x => new Property
                {
                    Id = x.Id,
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    Modified = x.Modified,
                    ModifiedBy = x.ModifiedBy,
                    Type = (Data.Enums.PropertyType)x.Type,
                    BookingEntityId = x.BookingEntityId,
                    Key = x.Key,
                    Value = x.Value
                }).ToList() : null;
                 record.Address = domainModel.Address != null ? new Address
                    {
                        Address1 = domainModel.Address.Address1,
                        Address2 = domainModel.Address.Address2,
                        Country = domainModel.Address.Country,
                        County = domainModel.Address.County,
                        Created = domainModel.Address.Created,
                        CreatedBy = domainModel.Address.CreatedBy,
                         Id = domainModel.Address.Id,
                        Modified = domainModel.Address.Modified,
                        ModifiedBy = domainModel.Address.ModifiedBy,
                        Number = domainModel.Address.Number,
                        Postcode = domainModel.Address.Postcode
                    } : null;
                
                
                if (record.Extras.Count>0)
                {
                    Repository.DeleteMany<Extra>(record.Extras.ToArray());

                    record.Extras = domainModel.Extras != null ? domainModel.Extras.Select(x => new Extra
                    {
                        Created = x.Created,
                        CreatedBy = x.CreatedBy,                   
                        Modified = x.Modified,
                        ModifiedBy = x.ModifiedBy,
                        BookingEntityId = x.BookingEntityId,
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description
                    }).ToList() : null;
                }
           
                if (record.Prices.Count>0)
                {
                    Repository.DeleteMany<BookingPricing>(record.Prices.ToArray());
                    
                    record.Prices = domainModel.Prices!=null?domainModel.Prices.Select(x=>new BookingPricing
                    {                         
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    Modified = x.Modified,
                    ModifiedBy = x.ModifiedBy,
                    Name = x.Name,
                    Start = x.Start,
                    End = x.End,               
                    DayPrices = x.DayPrices != null ? x.DayPrices.Select(r => new DayPrice
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
                                DayPriceId = m.DayPriceId                           
                            
                            }).ToList() : null,                            
                        }).ToList() : null,
                    }).ToList():null;
                }
                Repository.Update<BookingEntity>(record);
                Repository.Commit();

                return true;
        }

        public override bool DeleteById(int id)
        {
            var record = Repository.GetSingle<BookingEntity>(x => x.Id == id, x => x.Address, x => x.Extras, x => x.Properties, x => x.Prices.Select(e => e.DayPrices), x => x.Prices.Select(w => w.DayPrices.Select(y => y.HourPrices)));

            if (record.Extras.Count > 0)
            {
                Repository.DeleteMany<Extra>(record.Extras.ToArray());
            }
            if (record.Prices.Count > 0)
            {
                Repository.DeleteMany<BookingPricing>(record.Prices.ToArray());
            }

            if (record.Address!=null)
            {
                Repository.Delete<Address>(record.Address);
            }

            Repository.Delete<BookingEntity>(record);
            Repository.Commit();

            return true;
        }

        public async Task<BookingEntityModel> GetByName(string name)
        {
            var record = Repository.GetSingle<BookingEntity>(x => x.Name.Contains(name),x=>x.RootBookingEntity, x => x.Address, x => x.Extras,x=>x.Properties, x => x.Prices.Select(e => e.DayPrices), x => x.Prices.Select(w => w.DayPrices.Select(y => y.HourPrices)));

            return ToDomainModel(record);
        }

        public async Task<BookingEntityModel> GetByCode(string code)
        {
            var record = await Repository.GetSingleAsync<BookingEntity>(x => x.Code.ToLower()==code, x => x.RootBookingEntity, x => x.Address, x => x.Extras, x => x.Properties, x => x.Prices.Select(e => e.DayPrices), x => x.Prices.Select(w => w.DayPrices.Select(y => y.HourPrices)));

            return ToDomainModel(record);
        }
    }
}
