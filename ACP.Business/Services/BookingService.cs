using ACP.Business.Managers;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACP.Business.Models;

namespace ACP.Business.Services
{
    public class BookingService : IBookingService
    {
        private IBookingManager _bookingManager;
        private IAvailabilityManager _availability;
        private IBookingPricingManager _pricemanager;

        public BookingService(IBookingManager bookingManager, IAvailabilityManager availability)
        {
            _bookingManager = bookingManager;
            _availability = availability;
        }

        public async Task<BookingModel> Add(BookingModel model)
        {
            //## 1- Generate the Booking reference
            model.BookingReference = GenerateReference();

            //## 2- Generate the price
            var availability = _availability.FindAvailability(x => x.Status == Data.Enums.AvailabilityStatus.Free && x.StartDate == model.StartDate && x.EndDate == model.EndDate && x.Slot.BookingEntity.Code.ToLower().Contains(model.SourceCode.ToLower())).FirstOrDefault();
            var price =  _pricemanager.GetAllPricesByBookEntity(availability.Slot.BookingEntityId, model.EndDate, model.StartDate).FirstOrDefault();
            model.Price = price.DayPrices.Where(x => x.Day == Math.Round((model.EndDate - model.StartDate).TotalDays)).FirstOrDefault().Dayprice;
            
            //## 3- Block Slot
            //## 4- Block Availability
            //##    a) REMOVE AVAILABILITY I AM GETTINGS
            //##    b) FROM CURRENT availability START until My Availability (model) START CREATE A NEW AVAILABILITY -free
            //##    c) CREATE 1 WITH My availability (model) set - as occupied
            //##    d) CREATE FROM END my availability (model) until END in availability - free
            return await _bookingManager.AddAsync(model);

        }

        private string GenerateReference()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
            {
                i *= ((int)b + 1);
            }
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }


        public async Task<IList<BookingModel>> GetAll()
        {
            return _bookingManager.GetAll().ToList();
        }

        public async Task<BookingModel> GetById(int Id)
        {
            return await _bookingManager.GetByIdAsync(Id);
        }

        public async Task<BookingModel> GetByReference(string reference)
        {
           return await _bookingManager.GetByReference(reference);
        }

        public async Task<bool> Remove(int Id)
        {
            return await _bookingManager.DeleteByIdAsync(Id);
        }

        public async Task<bool> Update(BookingModel model)
        {
            return await _bookingManager.UpdateAsync(model);
        }
    }
}
