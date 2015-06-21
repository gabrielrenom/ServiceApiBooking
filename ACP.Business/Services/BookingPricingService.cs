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
        

        public bool UpdatePricesWithDays(int bookingEntityId, IList<BookingPricingModel> list)
        {
            return _bookingPricingManager.UpdatePricesWithDays( bookingEntityId,  list);
        }


        public IList<BookingPricingModel> GetAllPricesWithDays(int bookingEntityId)
        {
            return _bookingPricingManager.GetAllPricesWithDays(bookingEntityId);
        }

        public IList<BookingPricingModel> GetAllPricesWithDaysAndTimes(int bookingEntityId)
        {
            return _bookingPricingManager.GetAllPricesWithDaysAndTimes(bookingEntityId);
        }


        public bool DeleteById(int Id)
        {
            return _bookingPricingManager.DeleteById(Id);
        }
    }
}
