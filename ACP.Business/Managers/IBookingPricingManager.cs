using ACP.Business.Models;
using ACP.Business.Services;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Managers
{
    public interface IBookingPricingManager : IBaseManager<BookingPricingModel, BookingPricing>
    {
        IList<BookingPricingModel> GetAllPrices(DateTime pickup, DateTime dropoff);

        bool AddPricesWithDays(int bookingEntityId, IList<BookingPricingModel> prices);

        bool AddPricesWithDaysAndTime(int bookingEntityId, IList<BookingPricingModel> prices);        

        IList<BookingPricingModel> GetAllPricesWithDaysAndTimes(int bookingEntityId);

        IList<BookingPricingModel> GetAllPricesWithDays(int bookingEntityId);

        bool UpdatePricesWithDays(int bookingEntityId, IList<BookingPricingModel> list);
    }
}
