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
    public class RootBookingEntityManager : BaseACPManager<RootBookingEntityModel, RootBookingEntity>, IRootBookingEntityManager
    {
        public RootBookingEntityManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public override RootBookingEntityModel Add(RootBookingEntityModel domainModel)
        {
            return base.Add(domainModel);
        }

        public override RootBookingEntityModel GetById(int id)
        {
            return GetByIdIncluding(id, x => x.Status, x => x.Address, x => x.BookingEntities, x => x.BookingEntities.Select(y => y.Address), x => x.BookingEntities.Select(y => y.Properties));
        }

        public override bool Update(RootBookingEntityModel domainModel)
        {
            //var dataModel = Repository.GetSingle<RootBookingEntity>(a => a.Id == domainModel.Id, x => x.Status, x => x.Address, x => x.BookingEntities, x => x.BookingEntities.Select(y => y.Address));
            //var dataModel = Repository.GetSingle<RootBookingEntity>(a => a.Id == domainModel.Id, x => x.Status, x => x.Address);
            var dataModel = Repository.GetSingle<RootBookingEntity>(a => a.Id == domainModel.Id, x => x.Address, x => x.Status);
            dataModel.Code = domainModel.Code;
            dataModel.Website = domainModel.Website;
            dataModel.Created = domainModel.Created;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            //dataModel.Id = domainModel.Id;
            dataModel.AddressId = domainModel.Address!=null?domainModel.AddressId:0;
            if (domainModel.Address != null)
            {
                dataModel.AddressId = domainModel.AddressId;
                dataModel.Address.Address1 = domainModel.Address.Address1;
                dataModel.Address.Address2 = domainModel.Address.Address2;
                dataModel.Address.Country = domainModel.Address.Country;
                dataModel.Address.County = domainModel.Address.County;
                dataModel.Address.Created = domainModel.Address.Created;
                dataModel.Address.CreatedBy = domainModel.Address.CreatedBy;
                dataModel.Address.Id = domainModel.Address.Id;
                dataModel.Address.Modified = domainModel.Address.Modified;
                dataModel.Address.ModifiedBy = domainModel.Address.ModifiedBy;
                dataModel.Address.Number = domainModel.Address.Number;
                dataModel.Address.Postcode = domainModel.Address.Postcode;
                dataModel.Address.City = domainModel.Address.City;
              

            }
            //dataModel.StatusId = domainModel.Status!=null?domainModel.StatusId:0;

            var statusid = Repository.GetSingle<Status>(a => a.StatusType== (Data.Enums.StatusType)domainModel.Status.StatusType);
            dataModel.StatusId = statusid.Id;

            //if (domainModel.Status != null)
            //{
            //    dataModel.StatusId = domainModel.Status != null ? domainModel.StatusId : 0;
            //    dataModel.Status.Created = domainModel.Status.Created;
            //    dataModel.Status.CreatedBy = domainModel.Status.CreatedBy;
            //    dataModel.Status.Id = domainModel.Status.Id;
            //    dataModel.Status.Modified = domainModel.Status.Modified;
            //    dataModel.Status.ModifiedBy = domainModel.Status.ModifiedBy;
            //    dataModel.Status.StatusType = (Data.Enums.StatusType)domainModel.Status.StatusType;
            //}
            dataModel.Name = domainModel.Name;
            dataModel.Telephone = domainModel.Telephone;

            ////if (dataModel.BookingEntities.Count > 0)
            ////{
            ////    Repository.DeleteMany<BookingEntity>(dataModel.BookingEntities.ToArray());



            ////    dataModel.BookingEntities = domainModel.BookingEntities != null ? domainModel.BookingEntities.Select(r => new BookingEntity
            ////    {
            ////        Code = r.Code,
            ////        Comission = r.Comission,
            ////        Created = r.Created,
            ////        Id = r.Id,
            ////        RootBookingEntityId=r.RootBookEntityId,
            ////        CreatedBy = r.CreatedBy,
            ////        Image = r.Image,
            ////        Logo = r.Logo,
            ////        Modified = r.Modified,
            ////        ModifiedBy = r.ModifiedBy,
            ////        Name = r.Name,
            ////        Price = r.Price,
            ////        Sameday = r.Sameday,                    
            ////        Properties = r.Properties!=null?r.Properties.Select(y=> new Property{
            ////             Created= y.Created,
            ////              Id = y.Id,
            ////               Key = y.Key,
            ////                Value = y.Value,
            ////                 CreatedBy = y.CreatedBy,
            ////                  ModifiedBy = y.ModifiedBy,
            ////                   Modified = y.Modified,
            ////                    Type = (ACP.Data.Enums.PropertyType)y.Type
            ////        }).ToList():null,
            ////        //AddressId = r.Address==null?0:r.Address.Id,
            ////        Address =r.Address==null?null:new Address
            ////        {

            ////            Address1 = r.Address.Address1,
            ////            Address2 = r.Address.Address2,
            ////            Country = r.Address.Country,
            ////            County = r.Address.County,
            ////            Created = r.Address.Created,
            ////            CreatedBy = r.Address.CreatedBy,
            ////            Id = r.Address.Id,
            ////            Modified = r.Address.Modified,
            ////            ModifiedBy = r.Address.ModifiedBy,
            ////            Number = r.Address.Number,
            ////            Postcode = r.Address.Postcode,                        
            ////            City = r.Address.City

            ////        }
            ////    }).ToList() : null;
            ////}

            Repository.Update<RootBookingEntity>(dataModel);
            Repository.Commit();
            
            return true;
        }

        public override bool DeleteById(int id)
        {
            var dataModel = Repository.GetSingle<RootBookingEntity>(a => a.Id == id, x => x.Status, x => x.Address, x => x.BookingEntities, x => x.BookingEntities.Select(y => y.Address));

            
            if (dataModel.BookingEntities.Count > 0)
            {
                Repository.DeleteMany<BookingEntity>(dataModel.BookingEntities.ToArray());            
            }

            Repository.Delete<RootBookingEntity>(dataModel);
            Repository.Commit();

            return true;
   
        }

        public IList<RootBookingEntityModel> GetAll()
        {
            return GetListIncluding(x => x.Id > 0, x => x.Status, x => x.Address, x => x.BookingEntities,x=>x.Properties, x => x.BookingEntities.Select(y => y.Address), x => x.BookingEntities.Select(y => y.Properties)).ToList();            
        }

        public override RootBookingEntity ToDataModel(RootBookingEntityModel domainModel, RootBookingEntity dataModel = null)
        {
            if (dataModel == null)
            {
                dataModel = new RootBookingEntity();
            }
            dataModel.Code = domainModel.Code;
            dataModel.Website = domainModel.Website;
            dataModel.Created = domainModel.Created;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.Id = domainModel.Id;
            dataModel.AddressId = domainModel.AddressId;
            dataModel.Properties = domainModel.Properties!=null?domainModel.Properties.Select(x=>new RootBookingProperty
            {
                 Id = x.Id,
                 Created = x.Created,
                 CreatedBy = x.CreatedBy,
                 Modified = x.Modified,
                 ModifiedBy = x.ModifiedBy,
                 RootBookingEntityId = x.RootBookingEntityId,
                 Type = (Data.Enums.RootBookingPropertyType) x.PropertyType,
                 Key = x.Key,
                 Value= x.Value                  
            }).ToList():null;

            dataModel.Address = domainModel.Address!=null?new Address
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
            }:null;            
            dataModel.StatusId = domainModel.StatusId;
            dataModel.Status = domainModel.Status != null?new Status 
            {
                Created = domainModel.Status.Created,
                CreatedBy = domainModel.Status.CreatedBy,
                Id = domainModel.Status.Id,                
                Modified = domainModel.Status.Modified,
                ModifiedBy = domainModel.Status.ModifiedBy,
                StatusType = (Data.Enums.StatusType)domainModel.Status.StatusType
            }:null;
            dataModel.Name = domainModel.Name;
            dataModel.Telephone = domainModel.Telephone;
            dataModel.BookingEntities = domainModel.BookingEntities != null ? domainModel.BookingEntities.Select(r => new BookingEntity
            {
                Code=r.Code,
                Comission = r.Comission,
                Created = r.Created,
                Id = r.Id,
                RootBookingEntityId = r.RootBookEntityId,
                CreatedBy = r.CreatedBy,
                Image = r.Image,
                Logo = r.Logo,
                Modified = r.Modified,
                ModifiedBy = r.ModifiedBy,
                Name = r.Name,
                Price = r.Price,                
                Sameday = r.Sameday,
                AddressId = r.Address.Id,
                Address = new Address
                {
                    Address1 = r.Address.Address1,
                    Address2 = r.Address.Address2,
                    Country = r.Address.Country,
                    County = r.Address.County,
                    Created = r.Address.Created,
                    CreatedBy = r.Address.CreatedBy,
                    City= r.Address.City,
                    Id = r.Address.Id,
                    Modified = r.Address.Modified,
                    ModifiedBy = r.Address.ModifiedBy,
                    Number = r.Address.Number,
                    Postcode = r.Address.Postcode
                },
                Properties = r.Properties!=null? r.Properties.Select(x => new Property
                {
                    Id = x.Id,
                    Created = x.Created,
                    CreatedBy = x.CreatedBy,
                    Modified = x.Modified,
                    ModifiedBy = x.ModifiedBy,
                     BookingEntityId = x.BookingEntityId,
                    Type = (Data.Enums.PropertyType)x.Type,
                    Key = x.Key,
                    Value = x.Value
                }).ToList() : null,
        } ).ToList():null;            

            return dataModel;
        }
       

        public override RootBookingEntityModel ToDomainModel(RootBookingEntity dataModel)
        {
            RootBookingEntityModel model = new RootBookingEntityModel
            {
                 Website = dataModel.Website,
                  Code = dataModel.Code,
                Created = dataModel.Created, 
                Id = dataModel.Id,
                CreatedBy = dataModel.CreatedBy,
                Modified = dataModel.Modified,
                ModifiedBy = dataModel.ModifiedBy,
                Name = dataModel.Name,                
                Address = dataModel.Address!=null? new AddressModel
                {

                    Address1 = dataModel.Address.Address1,
                    Address2 = dataModel.Address.Address2,
                    Country = dataModel.Address.Country,
                    County = dataModel.Address.County,
                    Created = dataModel.Address.Created,
                    City = dataModel.Address.City,
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
                Status = dataModel.Status!=null? new StatusModel
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
                }).ToList() : null,
            BookingEntities = dataModel.BookingEntities != null ? dataModel.BookingEntities.Select(r => new BookingEntityModel
                {
                    Code=r.Code,
                    Comission = r.Comission,
                    Created = r.Created,
                    Id = r.Id,
                    RootBookEntityId=r.RootBookingEntityId,
                    CreatedBy = r.CreatedBy,
                    Image = r.Image,
                    Logo = r.Logo,
                    Modified = r.Modified,
                    ModifiedBy = r.ModifiedBy,
                    Name = r.Name,
                    Price = r.Price,
                    Sameday = r.Sameday,
                    Properties = r.Properties!=null?r.Properties.Select(y=>new PropertyModel
                    {
                        Key = y.Key,
                        Value = y.Value,
                        Type = (ACP.Business.Models.PropertyType) y.Type,
                        Id = y.Id,
                        Created = y.Created,
                        CreatedBy = y.CreatedBy,
                        Modified = y.Modified,
                        ModifiedBy = y.ModifiedBy,
                    }).ToList():null,
                    Address = r.Address!=null?new AddressModel
                    {
                        Address1 = r.Address.Address1,
                        Address2 = r.Address.Address2,
                        Country = r.Address.Country,
                        County = r.Address.County,
                        Created = r.Address.Created,
                        City = r.Address.City,
                        CreatedBy = r.Address.CreatedBy,
                        Id = r.Address.Id,
                        Modified = r.Address.Modified,
                        ModifiedBy = r.Address.ModifiedBy,
                        Number = r.Address.Number,
                        Postcode = r.Address.Postcode
                    }:null
                }).ToList() : null,

            };

            return model;
        }

        public override RootBookingEntityModel ToDomainModelWithChildNodes(RootBookingEntity dataModel)
        {
            RootBookingEntityModel model = new RootBookingEntityModel();

            model.Code = dataModel.Code;
                model.Created = dataModel.Created;
                model.Id = dataModel.Id;
                model.CreatedBy = dataModel.CreatedBy;
                model.Modified = dataModel.Modified;
                model.ModifiedBy = dataModel.ModifiedBy;
                model.Name = dataModel.Name;
                model.Website = dataModel.Website;
                model.Code = dataModel.Code;
                model.Address = new AddressModel
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
                };

                model.AddressId = dataModel.AddressId;
                model.StatusId = dataModel.StatusId;
                model.Telephone = dataModel.Telephone;
                model.Status = new StatusModel
                {
                    Created = dataModel.Status.Created,
                    CreatedBy = dataModel.Status.CreatedBy,
                    Id = dataModel.Status.Id,
                    Modified = dataModel.Status.Modified,
                    ModifiedBy = dataModel.Status.ModifiedBy,
                    StatusType = (Business.Enums.StatusType)dataModel.Status.StatusType
                };

                model.BookingEntities = dataModel.BookingEntities != null ? dataModel.BookingEntities.Select(r => new BookingEntityModel
                {
                    Code=r.Code,
                    Comission = r.Comission,
                    Created = r.Created,
                    Id = r.Id,
                    RootBookEntityId= r.RootBookingEntityId,
                    CreatedBy = r.CreatedBy,
                    Image = r.Image,
                    Logo = r.Logo,
                    Modified = r.Modified,
                    ModifiedBy = r.ModifiedBy,
                    Name = r.Name,
                    Price = r.Price,
                    Sameday = r.Sameday,
                    Address = new AddressModel
                    {
                        Address1 = r.Address.Address1,
                        Address2 = r.Address.Address2,
                        Country = r.Address.Country,
                        County = r.Address.County,
                        Created = r.Address.Created,
                        CreatedBy = r.Address.CreatedBy,
                        City = r.Address.City,
                        Id = r.Address.Id,
                        Modified = r.Address.Modified,
                        ModifiedBy = r.Address.ModifiedBy,
                        Number = r.Address.Number,
                        Postcode = r.Address.Postcode
                    }
                }).ToList() : null;            

            return model;
        }

        public async Task<RootBookingEntityModel> GetByName(string name)
        {
            var result= Repository.GetSingle<RootBookingEntity>(a => a.Name == name, x => x.Status, x => x.Address, x => x.BookingEntities, x => x.BookingEntities.Select(y => y.Address));
            return ToDomainModel(result);
        }

        public async Task<RootBookingEntityModel> GetByCode(string code)
        {
            var result = Repository.GetSingle<RootBookingEntity>(a => a.Code.ToLower() == code.ToLower(), x => x.Status, x => x.Address, x => x.BookingEntities, x => x.BookingEntities.Select(y => y.Address));
            return ToDomainModel(result);
        }
    }
}
