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

        public async Task<IList<BookingModel>> GetAll()
        {
            return _bookingManager.GetAll().ToList();
        }

        public async Task<BookingModel> GetById(int Id)
        {
            return await _bookingManager.GetByIdAsync(Id);
        }

        public async Task<bool> Remove(int Id)
        {
            return await _bookingManager.DeleteByIdAsync(Id);
        }

        public async Task<bool> Update(BookingModel model)
        {
            return await _bookingManager.UpdateAsync(model);
        }
    }
}
