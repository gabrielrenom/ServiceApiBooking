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
        Task<IList<BookingPricingModel>> GetAll();
        Task<bool> AddPricesWithDays(int bookingEntityId, IList<BookingPricingModel> prices);
        Task<bool> AddPricesWithDaysAndTimes(int bookingEntityId, IList<BookingPricingModel> prices);
        Task<IList<BookingPricingModel>> GetAllPricesWithDays(int bookingEntityId);
        Task<IList<BookingPricingModel>> GetAllPricesWithDaysAndTimes(int bookingEntityId);
        Task<bool> DeleteById(int Id);

        Task<bool> UpdatePricesWithDays(int p, IList<BookingPricingModel> list);
    }
}
