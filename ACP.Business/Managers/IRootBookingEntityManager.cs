using ACP.Business.Models;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Managers
{
    public interface IRootBookingEntityManager : IBaseManager<RootBookingEntityModel, RootBookingEntity>
    {
        IList<RootBookingEntityModel> GetAll();
        Task<RootBookingEntityModel> GetByName(string name);
    }
}
