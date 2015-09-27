using ACP.Business.Managers;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACP.Business.Models;

namespace ACP.Business.Services
{
    public class BookingService : IBookingService
    {
        private IBookingManager _bookingManager;

        public BookingService(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;            
        }

        public Task<BookingModel> Add(BookingModel model)
        {
            return _bookingManager.AddAsync(model);
        }

        public Task<IList<BookingModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<BookingModel> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(BookingModel model)
        {
            throw new NotImplementedException();
        }
    }
}
