using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACP.Business.Models;
using ACP.Business.Managers;

namespace ACP.Business.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerManager _customermanager;

        public CustomerService(ICustomerManager customermanager)
        {
            _customermanager = customermanager;
        }

        public async Task<CustomerModel> GetCustomerByEmail(string email)
        {
            return await _customermanager.GetCustomerByEmail(email);
        }
    }
}
