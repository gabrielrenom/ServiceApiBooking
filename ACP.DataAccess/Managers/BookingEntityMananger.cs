using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Managers
{
    public class BookingEntityMananger : BaseACPManager<BookingEntityModel, BookingEntity>, IBookingEntityManager
    {
    }
}
