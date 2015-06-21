using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IBookingEntityService
    {
        bool Add(BookingEntityModel model);
        bool Update(BookingEntityModel model);
        IList<BookingEntityModel> GetAllBookingEntities();
        BookingEntityModel GetBookingEntityById(int Id);
        bool Remove(int Id);
    }
}
