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
    public class BookingEntityService : IBookingEntityService
    {
        private readonly IBookingEntityManager _bookingEntityManager;

        public BookingEntityService(IBookingEntityManager bookingEntityManager)
        {
            _bookingEntityManager = bookingEntityManager;
        }

        public async Task<bool> Add(BookingEntityModel model)
        {
            BookingEntityModel added= _bookingEntityManager.Add(model);

            return (added.Id!=null?true:false);
        }

        public async Task<bool> Update(BookingEntityModel model)
        {
            return _bookingEntityManager.Update(model);
        }

        public async Task<IList<Models.BookingEntityModel>> GetAllBookingEntities()
        {
            return _bookingEntityManager.GetAllBookingEntities().ToList();            
        }

        public async Task<Models.BookingEntityModel> GetBookingEntityById(int Id)
        {
            return _bookingEntityManager.GetById(Id);
        }

        public async Task<bool> Remove(int Id)
        {
            return _bookingEntityManager.DeleteById(Id);
        }

        public async Task<IList<AvailabilityModel>> GetAvailableSpacesById(int Id, DateTime StartDate, DateTime EndDate,string Status=null)
        {
            if (Status!=null)
                return _bookingEntityManager.GetById(Id).Availability.Where(x => StartDate > x.StartDate && EndDate < x.EndDate && x.Status.Name == Status).ToList();
            else
                return _bookingEntityManager.GetById(Id).Availability.Where(x => StartDate > x.StartDate && EndDate < x.EndDate).ToList();
             
        }

        public Task<BookingEntityModel> GetByName(string name)
        {
            return _bookingEntityManager.GetByName(name);
        }
    }
}
