using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IZoneService
    {
        Task<ZoneModel> Add(ZoneModel model);
        Task<bool> Update(ZoneModel model);
        Task<IList<ZoneModel>> GetAll();
        Task<IList<ZoneModel>> GetAllOccupied();
        Task<IList<ZoneModel>> GetAllFree();
        Task<ZoneModel> GetById(int Id);
        Task<ZoneModel> GetByNumberIdentifier(int? number=null, string identifier=null);
        Task<bool> Remove(int Id);
        Task<IList<ZoneModel>> FindZoneAvailable(DateTime startdate, DateTime enddate);
    }
}
