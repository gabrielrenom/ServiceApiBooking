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
    public class UserManager : BaseACPManager<UserModel, User>, IUserManager
    {
        public UserManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public override UserModel Add(UserModel domainModel)
        {
            return base.Add(domainModel);
        }

        public override User ToDataModel(UserModel domainModel, User dataModel = null)
        {            
            if (dataModel == null)
                dataModel = new User();
          
            dataModel.Created = domainModel.Created;
            dataModel.Id = domainModel.Id;
            dataModel.CreatedBy = domainModel.CreatedBy;
            dataModel.Modified = domainModel.Modified;
            dataModel.ModifiedBy = domainModel.ModifiedBy;
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
            dataModel.Email = domainModel.Email;
            dataModel.FirstName = domainModel.FirstName;
            dataModel.LastName = domainModel.LastName;
            dataModel.Password = domainModel.Password;
            dataModel.PhoneNumber = domainModel.PhoneNumber;
            dataModel.AddressId = domainModel.AddressId;
            dataModel.Cars=domainModel.Cars!=null?domainModel.Cars.Select(x=>new Car{
                Created = x.Created,
                CreatedBy = x.CreatedBy,
                Id = x.Id,
                Modified = x.Modified,
                ModifiedBy = x.ModifiedBy,
                Colour = x.Colour,
                Make = x.Make,
                Model = x.Model,
                Registration = x.Registration                     
            }).ToList():null;   
            return dataModel;            
        }

        public override UserModel ToDomainModel(User dataModel)
        {
            UserModel model = new UserModel
            {                
                Created = dataModel.Created,
                Id = dataModel.Id,
                CreatedBy = dataModel.CreatedBy,
                Modified = dataModel.Modified,
                ModifiedBy = dataModel.ModifiedBy,
                AddressId = dataModel.AddressId,
                Address = dataModel.Address != null ? new AddressModel
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
                } : null,
                 PhoneNumber = dataModel.PhoneNumber,
                  FirstName = dataModel.FirstName,
                  LastName = dataModel.LastName,
                  Email = dataModel.Email,
                  Password = dataModel.Password,
                  Cars = dataModel.Cars!=null?dataModel.Cars.Select(x=>new CarModel{
                         Created = x.Created,
                         CreatedBy = x.CreatedBy,
                         Id = x.Id,
                         Modified = x.Modified,
                         ModifiedBy = x.ModifiedBy,
                         Colour = x.Colour,
                         Make = x.Make,
                         Model = x.Model,
                         Registration = x.Registration                     
                  }).ToList():null                     
            };

            return model;
        }

        public IList<UserModel> GetAllUsersWithCarsAndBookings()
        {
            //Bookings needs to be implemented in the domain
            return GetListIncluding(x => x.Id > 0, x => x.Bookings, x => x.Cars, x=>x.Address).ToList();
        }

        public IList<UserModel> GetAllUsersWithCars()
        {
            return GetListIncluding(x => x.Id > 0, x => x.Cars,x=>x.Address).ToList();
        }
    }
}
