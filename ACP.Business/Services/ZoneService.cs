using ACP.Business.Managers;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ACP.Business.Models;

namespace ACP.Business.Services
{
    public class ZoneService: IZoneService
    {
        private IZoneManager _zonemanager;
        public ZoneService(IZoneManager zonemanager)
        {
            _zonemanager = zonemanager;
        }

        public async Task<ZoneModel> Add(ZoneModel model)
        {
            return await _zonemanager.AddAsync(model);
        }

        public async Task<IList<ZoneModel>> GetAll()
        {
            var result = await _zonemanager.GetAllAsync();
            return result.ToList();
        }

        public async Task<IList<ZoneModel>> GetAllFree()
        {
            return await _zonemanager.GetAllFreeAsync();
        }

        public async Task<IList<ZoneModel>> GetAllOccupied()
        {
            return await _zonemanager.GetAllOccupiedAsync();
        }

        public async Task<ZoneModel> GetById(int Id)
        {
            return await _zonemanager.GetByIdAsync(Id);
        }

        public async Task<ZoneModel> GetByNumberIdentifier(int? number = null, string identifier = null)
        {
            return await _zonemanager.GetByNumberIdentifierAsync(number, identifier);
        }

        public async Task<bool> Remove(int Id)
        {
            return await _zonemanager.DeleteByIdAsync(Id);
        }

        public async Task<bool> Update(ZoneModel model)
        {
            return await _zonemanager.UpdateAsync(model);
        }
    }
}
