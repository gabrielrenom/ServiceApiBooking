﻿using ACP.Business.Models;
using ACP.Business.Services;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        IList<BookingPricingModel> GetAllPricesByBookEntity(int bookingentityid,DateTime dropoff, DateTime pickup);

        IList<BookingPricingModel> GetAllPricesByPickLocationAndDropLocation(string pickuplocation, string droplocation, DateTime pickup, DateTime dropoff);

        IList<BookingPricingModel> GetAllPricesAndReviewsByPickLocationAndDropLocationByName(string pickuplocation, string droplocation, DateTime pickup, DateTime dropoff);
        Task<IList<BookingPricingModel>> GetAllPricesAndReviewsByPickLocationAndDropLocationById(int pickuplocationid, int droplocationid, DateTime dropoff, DateTime pickup);
        Task<IEnumerable<BookingPricingModel>> GetAllIncludingAsync(params Expression<Func<BookingPricing, object>>[] navigationProperties);

    }
}
