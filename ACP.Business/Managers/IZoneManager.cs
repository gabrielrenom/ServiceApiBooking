using ACP.Business.Models;
using ACP.Data.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Managers
{
    public interface IZoneManager : IBaseManager<ZoneModel, Zone>
    {
        Task<IList<ZoneModel>> GetAllFreeAsync();
        Task<IList<ZoneModel>> GetAllOccupiedAsync();
        Task<ZoneModel> GetByNumberIdentifierAsync(int? number = null, string identifier = null);
    }
}
