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
    public class BookingPricingService : IBookingPricingService
    {
        private IBookingPricingManager _bookingPricingManager;

        public BookingPricingService(IBookingPricingManager bookingPricingManager)
        {
            _bookingPricingManager = bookingPricingManager;
        }

        public IEnumerable<BookingPricingModel> GetAll()
        {
            return _bookingPricingManager.GetAll();
        }
    }
}
