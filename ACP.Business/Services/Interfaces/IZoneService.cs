using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface ISlotService
    {
        Task<SlotModel> Add(SlotModel model);
        Task<bool> Update(SlotModel model);
        Task<IList<SlotModel>> GetAll();
        Task<IList<SlotModel>> GetAllOccupied();
        Task<IList<SlotModel>> GetAllFree();
        Task<SlotModel> GetById(int Id);
        Task<SlotModel> GetByNumberIdentifier(int? number=null, string identifier=null);
        Task<bool> Remove(int Id);
        Task<IList<SlotModel>> FindSlotAvailable(DateTime startdate, DateTime enddate, string rootname);
        Task<SlotModel> GetByIdWithAllAvailabilities(int id);
    }
}
