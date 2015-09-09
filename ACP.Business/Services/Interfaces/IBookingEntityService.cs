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
        Task<bool> Add(BookingEntityModel model);
        Task<bool> Update(BookingEntityModel model);
        Task<IList<BookingEntityModel>> GetAllBookingEntities();
        Task<BookingEntityModel> GetBookingEntityById(int Id);
        Task<bool> Remove(int Id);
    }
}
