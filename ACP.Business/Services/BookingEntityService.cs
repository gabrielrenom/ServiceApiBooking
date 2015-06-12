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

        public bool Add(BookingEntityModel model)
        {
            BookingEntityModel added= _bookingEntityManager.Add(model);

            return (added.Id!=null?true:false);
        }

        public bool Update(BookingEntityModel model)
        {
            return _bookingEntityManager.Update(model);
        }

        public IList<Models.BookingEntityModel> GetAllBookingEntities()
        {
            return _bookingEntityManager.GetAllBookingEntities().ToList();            
        }

        public Models.BookingEntityModel GetBookingEntityById(int Id)
        {
            return _bookingEntityManager.GetById(Id);
        }

        public bool Remove(int Id)
        {
            return _bookingEntityManager.DeleteById(Id);
        }
    }
}
