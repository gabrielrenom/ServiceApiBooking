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
    public class SlotService: ISlotService
    {
        private ISlotManager _Slotmanager;
        public SlotService(ISlotManager Slotmanager)
        {
            _Slotmanager = Slotmanager;
        }

        public async Task<SlotModel> Add(SlotModel model)
        {
            return await _Slotmanager.AddAsync(model);
        }

        public async Task<IList<SlotModel>> FindSlotAvailable(DateTime startdate, DateTime enddate, string rootname)
        {
            return await _Slotmanager.FindSlotAvailable(startdate,enddate,rootname);
        }

        public async Task<IList<SlotModel>> GetAll()
        {
            var result = await _Slotmanager.GetAllAsync();
            return result.ToList();
        }

        public async Task<IList<SlotModel>> GetAllFree()
        {
            return await _Slotmanager.GetAllFreeAsync();
        }

        public async Task<IList<SlotModel>> GetAllOccupied()
        {
            return await _Slotmanager.GetAllOccupiedAsync();
        }

        public async Task<SlotModel> GetById(int Id)
        {
            return await _Slotmanager.GetByIdAsync(Id);
        }

        public async Task<SlotModel> GetByNumberIdentifier(int? number = null, string identifier = null)
        {
            return await _Slotmanager.GetByNumberIdentifierAsync(number, identifier);
        }

        public async Task<bool> Remove(int Id)
        {
            return await _Slotmanager.DeleteByIdAsync(Id);
        }

        public async Task<bool> Update(SlotModel model)
        {
            //return await _Slotmanager.UpdateAsync(model);
            return _Slotmanager.Update(model);
        }
    }
}
