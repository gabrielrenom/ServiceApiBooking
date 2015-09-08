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
            var dataModel = Repository.GetSingle<RootBookingEntity>(a => a.Id == domainModel.Id, x => x.Status, x => x.Address, x => x.BookingEntities, x => x.BookingEntities.Select(y => y.Address));
            
            dataModel.Created = domainModel.Created;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
            dataModel.Id = domainModel.Id;
            dataModel.AddressId = domainModel.AddressId;
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
            dataModel.StatusId = domainModel.StatusId;
            dataModel.Status = new Status
            {
                Created = domainModel.Status.Created,
                CreatedBy = domainModel.Status.CreatedBy,
                Id = domainModel.Status.Id,
                Modified = domainModel.Status.Modified,
                ModifiedBy = domainModel.Status.ModifiedBy,
                Name = domainModel.Status.Name
            };
            dataModel.Name = domainModel.Name;
            dataModel.Telephone = domainModel.Telephone;

            if (dataModel.BookingEntities.Count > 0)
            {
                Repository.DeleteMany<BookingEntity>(dataModel.BookingEntities.ToArray());


                dataModel.BookingEntities = domainModel.BookingEntities != null ? domainModel.BookingEntities.Select(r => new BookingEntity
                {
                    Comission = r.Comission,
                    Created = r.Created,
                    Id = r.Id,
                    CreatedBy = r.CreatedBy,
                    Image = r.Image,
                    Logo = r.Logo,
                    Modified = r.Modified,
                    ModifiedBy = r.ModifiedBy,
                    Name = r.Name,
                    Price = r.Price,
                    Sameday = r.Sameday,                    
                    Properties = r.Properties!=null?r.Properties.Select(y=> new Property{
                         Created= y.Created,
                          Id = y.Id,
                           Key = y.Key,
                            Value = y.Value,
                             CreatedBy = y.CreatedBy,
                              ModifiedBy = y.ModifiedBy,
                               Modified = y.Modified,
                                Type = (ACP.Data.Enums.PropertyType)y.Type
                    }).ToList():null,
                    //AddressId = r.Address==null?0:r.Address.Id,
                    Address =r.Address==null?null:new Address
                    {

                        Address1 = r.Address.Address1,
                        Address2 = r.Address.Address2,
                        Country = r.Address.Country,
                        County = r.Address.County,
                        Created = r.Address.Created,
                        CreatedBy = r.Address.CreatedBy,
                        Id = r.Address.Id,
                        Modified = r.Address.Modified,
                        ModifiedBy = r.Address.ModifiedBy,
                        Number = r.Address.Number,
                        Postcode = r.Address.Postcode,
                        City = r.Address.City

                    }
                }).ToList() : null;
            }

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
            return GetListIncluding(x => x.Id > 0, x => x.Status, x => x.Address, x => x.BookingEntities, x => x.BookingEntities.Select(y => y.Address), x => x.BookingEntities.Select(y => y.Properties)).ToList();            
        }

        public override RootBookingEntity ToDataModel(RootBookingEntityModel domainModel, RootBookingEntity dataModel = null)
        {
            if (dataModel == null)
            {
                dataModel = new RootBookingEntity();
            }

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
            dataModel.Status = domainModel.Address != null?new Status 
            {
                Created = domainModel.Status.Created,
                CreatedBy = domainModel.Status.CreatedBy,
                Id = domainModel.Status.Id,
                Modified = domainModel.Status.Modified,
                ModifiedBy = domainModel.Status.ModifiedBy,
                Name = domainModel.Status.Name 
            }:null;
            dataModel.Name = domainModel.Name;
            dataModel.Telephone = domainModel.Telephone;
            dataModel.BookingEntities = domainModel.BookingEntities != null ? domainModel.BookingEntities.Select(r => new BookingEntity
            {
                Comission = r.Comission,
                Created = r.Created,
                Id = r.Id,
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
                    Id = r.Address.Id,
                    Modified = r.Address.Modified,
                    ModifiedBy = r.Address.ModifiedBy,
                    Number = r.Address.Number,
                    Postcode = r.Address.Postcode
                }
            } ).ToList():null;            

            return dataModel;
        }
       

        public override RootBookingEntityModel ToDomainModel(RootBookingEntity dataModel)
        {
            RootBookingEntityModel model = new RootBookingEntityModel
            {
                Created = dataModel.Created, 
                Id = dataModel.Id,
                CreatedBy = dataModel.CreatedBy,
                Modified = dataModel.Modified,
                ModifiedBy = dataModel.ModifiedBy,
                Name = dataModel.Name,                
                Address = new AddressModel
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
                },
                AddressId = dataModel.AddressId,
                StatusId = dataModel.StatusId,
                Telephone = dataModel.Telephone,
                Status = new StatusModel
                {
                    Created = dataModel.Status.Created,
                    CreatedBy = dataModel.Status.CreatedBy,
                    Id = dataModel.Status.Id,
                    Modified = dataModel.Status.Modified,
                    ModifiedBy = dataModel.Status.ModifiedBy,
                    Name = dataModel.Status.Name
                },

                BookingEntities = dataModel.BookingEntities != null ? dataModel.BookingEntities.Select(r => new BookingEntityModel
                {
                    Comission = r.Comission,
                    Created = r.Created,
                    Id = r.Id,
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
                        Type = (ACP.Business.Models.PropertyType) y.Type
                    }).ToList():null,
                    Address = new AddressModel
                    {
                        Address1 = r.Address.Address1,
                        Address2 = r.Address.Address2,
                        Country = r.Address.Country,
                        County = r.Address.County,
                        Created = r.Address.Created,
                        CreatedBy = r.Address.CreatedBy,
                        Id = r.Address.Id,
                        Modified = r.Address.Modified,
                        ModifiedBy = r.Address.ModifiedBy,
                        Number = r.Address.Number,
                        Postcode = r.Address.Postcode
                    }
                }).ToList() : null,

            };

            return model;
        }

        public override RootBookingEntityModel ToDomainModelWithChildNodes(RootBookingEntity dataModel)
        {
            RootBookingEntityModel model = new RootBookingEntityModel();

            
                model.Created = dataModel.Created;
                model.Id = dataModel.Id;
                model.CreatedBy = dataModel.CreatedBy;
                model.Modified = dataModel.Modified;
                model.ModifiedBy = dataModel.ModifiedBy;
                model.Name = dataModel.Name;
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
                    Postcode = dataModel.Address.Postcode
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
                    Name = dataModel.Status.Name
                };

                model.BookingEntities = dataModel.BookingEntities != null ? dataModel.BookingEntities.Select(r => new BookingEntityModel
                {
                    Comission = r.Comission,
                    Created = r.Created,
                    Id = r.Id,
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
                        Id = r.Address.Id,
                        Modified = r.Address.Modified,
                        ModifiedBy = r.Address.ModifiedBy,
                        Number = r.Address.Number,
                        Postcode = r.Address.Postcode
                    }
                }).ToList() : null;            

            return model;
        }
    }
}
