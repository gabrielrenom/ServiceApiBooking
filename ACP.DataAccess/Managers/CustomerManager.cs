using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACP.Business.Repository;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ACP.DataAccess.Managers
{
    public class CustomerManager :  BaseACPManager<CustomerModel, Customer>, ICustomerManager
    {
        
        public CustomerManager(IACPRepository repository)
            : base(repository)
        {
            Repository = repository;
        }

        public async Task<CustomerModel> GetCustomerByEmail(string email)
        {
            return await GetSingleAsync(x => x.Email.ToLower() == email.ToLower());
        }

        public override CustomerModel ToDomainModel(Customer dataModel)
        {
            return new CustomerModel
            {
                 Address= dataModel.Address!=null?new AddressModel { }:null,
                  AddressId = dataModel.Id,
                   CreatedBy = dataModel.CreatedBy,
                    Email = dataModel.Email,
                     Created = dataModel.Created,
                      Fax = dataModel.Fax,
                       Forename = dataModel.Forename,
                        Initials = dataModel.Initials,
                         Mobile = dataModel.Mobile,
                           Surname = dataModel.Surname,
                            ModifiedBy = dataModel.ModifiedBy,
                             Telephone = dataModel.Telephone,
                              Modified = dataModel.Modified,
                               Title = dataModel.Title

            };
        }
    }
}
