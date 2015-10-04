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
        private ISlotManager _slotmanager;


        public BookingService(IBookingManager bookingManager, IAvailabilityManager availability, ISlotManager slotmanager, IBookingPricingManager pricemanager)
        {
            _bookingManager = bookingManager;
            _availability = availability;
            _slotmanager = slotmanager;
            _pricemanager = pricemanager;
        }

        public async Task<BookingModel> Add(BookingModel model)
        {
            
            //## 1- Generate the Booking reference
            model.BookingReference = GenerateReference();

            //## 2- Find the slot
            var slots = await _slotmanager.FindSlotAvailable(model.StartDate, model.EndDate, model.SourceCode);
            
            //## 3- Generate the price
            var price =  _pricemanager.GetAllPricesByBookEntity(slots.FirstOrDefault().BookingEntityId, model.EndDate, model.StartDate).FirstOrDefault();
            model.Price = price.DayPrices.Where(x => x.Day == Math.Round((model.EndDate - model.StartDate).TotalDays)).FirstOrDefault().Dayprice;

            //## 4- Booking the slot
            AvailabilityModel availability = new AvailabilityModel();
            availability.StartDate = model.StartDate;
            availability.EndDate = model.EndDate;
            availability.Status = Enums.AvailabilityStatus.Occupied;
            availability.SlotId = slots.FirstOrDefault().Id;

            //## 5- Add in Bookink Links the new Slot and Booking
            var newavailability = await _availability.AddAsync(availability);
            model.BookingLink = new List<BookingLinkModel>
            {
                new BookingLinkModel {  SlotId = newavailability.SlotId, AvailabilityId= newavailability.Id}
            };

            //## 6- FINALLY Adding the booking.
            return await _bookingManager.AddAsync(model);

        }

        public async Task<BookingModel> Add(BookingModel model, bool IsBookingEntity)
        {
            IList<SlotModel> slots = new List<SlotModel>();
            //## 1- Generate the Booking reference
            model.BookingReference = GenerateReference();

            //## 2- Find the slot
            if (!IsBookingEntity)
                slots = await _slotmanager.FindSlotAvailable(model.StartDate, model.EndDate, model.SourceCode);
            else
                slots = await _slotmanager.FindSlotAvailableByBookingEntityCode(model.StartDate, model.EndDate, model.SourceCode);

            //## 3- Generate the price
            var price = _pricemanager.GetAllPricesByBookEntity(slots.FirstOrDefault().BookingEntityId, model.EndDate, model.StartDate).FirstOrDefault();
            model.Price = price.DayPrices.Where(x => x.Day == Math.Round((model.EndDate - model.StartDate).TotalDays)).FirstOrDefault().Dayprice;

            //## 4- Booking the slot
            AvailabilityModel availability = new AvailabilityModel();
            availability.StartDate = model.StartDate;
            availability.EndDate = model.EndDate;
            availability.Status = Enums.AvailabilityStatus.Occupied;
            availability.SlotId = slots.FirstOrDefault().Id;

            //## 5- Add in Bookink Links the new Slot and Booking
            var newavailability = await _availability.AddAsync(availability);
            model.BookingLink = new List<BookingLinkModel>
            {
                new BookingLinkModel {  SlotId = newavailability.SlotId, AvailabilityId= newavailability.Id}
            };

            //## 6- FINALLY Adding the booking.
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
