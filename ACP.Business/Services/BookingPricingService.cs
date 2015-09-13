﻿using ACP.Business.Managers;
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

        public async Task<IList<BookingPricingModel>> GetAll()
        {
            return _bookingPricingManager.GetAll().ToList();
        }


        public async Task<bool> AddPricesWithDays(int bookingEntityId, IList<BookingPricingModel> prices)
        {
            return _bookingPricingManager.AddPricesWithDays(bookingEntityId, prices);
        }

        public async Task<bool> AddPricesWithDaysAndTimes(int bookingEntityId, IList<BookingPricingModel> prices)
        {
            throw new NotImplementedException();
        }
        

        public async Task<bool> UpdatePricesWithDays(int bookingEntityId, IList<BookingPricingModel> list)
        {
            return _bookingPricingManager.UpdatePricesWithDays( bookingEntityId,  list);
        }


        public async Task<IList<BookingPricingModel>> GetAllPricesWithDays(int bookingEntityId)
        {
            return _bookingPricingManager.GetAllPricesWithDays(bookingEntityId);
        }

        public async Task<IList<BookingPricingModel>> GetAllPricesWithDaysAndTimes(int bookingEntityId)
        {
            return _bookingPricingManager.GetAllPricesWithDaysAndTimes(bookingEntityId);
        }


        public async Task<bool> DeleteById(int Id)
        {
            return _bookingPricingManager.DeleteById(Id);
        }
    }
}
