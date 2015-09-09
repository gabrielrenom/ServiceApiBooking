using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IAvailabilityService
    {
        Task<AvailabilityModel> Add(AvailabilityModel model);
        Task<bool> Remove(int Id);
        Task<bool> Update(AvailabilityModel model);
        Task<AvailabilityModel> GetById(int Id);
        Task<IList<AvailabilityModel>> GetAll();
    }
}
