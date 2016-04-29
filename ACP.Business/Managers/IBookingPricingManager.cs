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

        IList<BookingPricingModel> GetAllPricesByBookEntity(int bookingentityid, DateTime pickup, DateTime dropoff);

        IList<BookingPricingModel> GetAllPricesByPickLocationAndDropLocation(string pickuplocation, string droplocation, DateTime pickup, DateTime dropoff);

        IList<BookingPricingModel> GetAllPricesAndReviewsByPickLocationAndDropLocation(string pickuplocation, string droplocation, DateTime pickup, DateTime dropoff);
    }
}
