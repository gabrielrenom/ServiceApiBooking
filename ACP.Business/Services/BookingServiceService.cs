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
    }
}
