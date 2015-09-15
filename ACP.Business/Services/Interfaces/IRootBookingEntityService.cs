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
        Task<bool> Update(RootBookingEntityModel model);
        Task<IList<RootBookingEntityModel>> GetAll();
        Task<Models.RootBookingEntityModel> GetById(int Id);
        Task<bool> Remove(int Id);
        Task<RootBookingEntityModel> GetByName(string name);
    }
}
