using ACP.Business.Models;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Managers
{
    public interface IBookingManager : IBaseManager<BookingModel, Booking>
    {
        Task<BookingModel> GetByReference(string reference);
        Task<bool> Paid(int id);
        BookingModel GetModel();
    }
}
