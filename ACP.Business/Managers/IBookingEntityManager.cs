using ACP.Business.Models;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ACP.Business.Managers
{
    public interface IBookingEntityManager:IBaseManager<BookingEntityModel, BookingEntity>
    {
        IList<BookingEntityModel> GetAllBookingEntities();
    }
}
