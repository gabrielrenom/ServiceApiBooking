using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly IAvailabilityManager _availabilityManager;
        private readonly ISlotManager _SlotManager;

        public AvailabilityService(IAvailabilityManager availabilityManager)
        {
            _availabilityManager = availabilityManager;
        }

        public async Task<AvailabilityModel> Add(AvailabilityModel model)
        {
           return _availabilityManager.Add(model);  
        }

        public async Task<bool> Remove(int Id)
        {
            return _availabilityManager.DeleteById(Id);
        }

        public async Task<bool> Update(AvailabilityModel model)
        {
            return _availabilityManager.Update(model);
        }

        public async Task<AvailabilityModel> GetById(int Id)
        {
            return _availabilityManager.GetById(Id);
        }

        public async Task<IList<AvailabilityModel>> GetAll()
        {
            return _availabilityManager.GetAll().ToList();
        }

        public async Task<IList<AvailabilityModel>> GetByAvailability(AvailabilityModel model)
        {            
            return  _availabilityManager.FindAvailability(x => x.Status.StatusType==(Data.Enums.StatusType) model.Status.StatusType && x.StartDate == model.StartDate && x.EndDate == model.EndDate);
        }
    }
}
