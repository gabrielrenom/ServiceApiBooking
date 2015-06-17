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
        private IBookingEntityManager _bookingEntityManager;

        public BookingPricingService(IBookingPricingManager bookingPricingManager, IBookingEntityManager bookingEntityManager)
        {
            _bookingPricingManager = bookingPricingManager;
            _bookingEntityManager = bookingEntityManager;
        }

        public IEnumerable<BookingPricingModel> GetAll()
        {
            return _bookingPricingManager.GetAll();
        }


        public bool AddPricesWithDays(int bookingEntityId, IList<BookingPricingModel> prices)
        {
            return _bookingPricingManager.AddPricesWithDays(bookingEntityId, prices);
        }

        public bool AddPricesWithDaysAndTimes(int bookingEntityId, IList<BookingPricingModel> prices)
        {
            throw new NotImplementedException();
        }

        public BookingEntityModel GetAllPricesWithDays(int bookingEntityId)
        {
            throw new NotImplementedException();
        }

        public BookingEntityModel GetAllPricesWithDaysAndTimes(int bookingEntityId)
        {
            throw new NotImplementedException();
        }
    }
}
