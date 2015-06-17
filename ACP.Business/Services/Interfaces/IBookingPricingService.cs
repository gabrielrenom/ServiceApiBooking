using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IBookingPricingService
    {
        IEnumerable<BookingPricingModel> GetAll();
        bool AddPricesWithDays(int bookingEntityId, IList<BookingPricingModel> prices);
        bool AddPricesWithDaysAndTimes(int bookingEntityId, IList<BookingPricingModel> prices);
        BookingEntityModel GetAllPricesWithDays(int bookingEntityId);
        BookingEntityModel GetAllPricesWithDaysAndTimes(int bookingEntityId);

    }
}
