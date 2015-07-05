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
        AvailabilityModel Add(AvailabilityModel model);
        bool Remove(int Id);
        bool Update(AvailabilityModel model);
        AvailabilityModel GetById(int Id);
        IList<AvailabilityModel> GetAll();
    }
}
