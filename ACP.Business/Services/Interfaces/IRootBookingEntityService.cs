using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IRootBookingEntityService
    {
        Task<RootBookingEntityModel> Add(RootBookingEntityModel model);
        bool Update(RootBookingEntityModel model);
        IList<RootBookingEntityModel> GetAll();
        Task<Models.RootBookingEntityModel> GetById(int Id);
        bool Remove(int Id);
    }
}
