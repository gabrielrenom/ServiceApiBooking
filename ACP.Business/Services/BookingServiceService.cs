using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services
{
    public class BookingServiceService : IBookingServiceService
    {
        IBookingServiceManager _serviceManager;
        public BookingServiceService(IBookingServiceManager servicemanager)
        {
            _serviceManager = servicemanager;
        }

        public BookingServiceModel Add(BookingServiceModel service)
        {
            return _serviceManager.Add(service);
        }


        public Task<BookingServiceModel> AddAsync(BookingServiceModel service)
        {
            return _serviceManager.AddAsync(service);
        }
        

        public BookingServiceModel GetServiceByName(string servicename)
        {
            return _serviceManager.GetSingle(x => x.Name == servicename);
        }

        public Task<BookingServiceModel> GetServiceByNameAsync(string servicename)
        {
            return _serviceManager.GetSingleAsync(x => x.Name == servicename);
        }


        public bool UpdateService(BookingServiceModel model)
        {
            return _serviceManager.Update(model);
        }

        public Task<bool> UpdateServiceAsync(BookingServiceModel model)
        {
            return _serviceManager.UpdateAsync(model);
        }
          

        public bool RemoveService( int id)
        {
            return _serviceManager.DeleteById(id);
        }

        public Task<bool> RemoveServiceAsync(int id)
        {
            return _serviceManager.DeleteByIdAsync(id);
        }


        public IList<BookingServiceModel> GetAll()
        {            
            return _serviceManager.GetAll().ToList<BookingServiceModel>();
        }

        public Task<IEnumerable<BookingServiceModel>> GetAllAsync()
        {
            return _serviceManager.GetAllAsync();
        }
    }
}
