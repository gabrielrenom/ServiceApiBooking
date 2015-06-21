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
        IList<BookingPricingModel> GetAllPricesWithDays(int bookingEntityId);
        IList<BookingPricingModel> GetAllPricesWithDaysAndTimes(int bookingEntityId);
        bool DeleteById(int Id);

        bool UpdatePricesWithDays(int p, IList<BookingPricingModel> list);
    }
}
