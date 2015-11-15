using ACP.Business.Models;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Managers
{
    public interface ISlotManager : IBaseManager<SlotModel, Slot>
    {
        Task<IList<SlotModel>> GetAllFreeAsync();
        Task<IList<SlotModel>> GetAllOccupiedAsync();
        Task<SlotModel> GetByNumberIdentifierAsync(int? number = null, string identifier = null);
        Task<IList<SlotModel>> FindSlotAvailable(DateTime startdate, DateTime enddate, string rootname);
        Task<IList<SlotModel>> GetAllFreeAsync(string bookingentitycode);
        Task<IList<SlotModel>> FindSlotAvailableByBookingEntityCode(DateTime startdate, DateTime enddate, string code);
        Task<SlotModel> GetWithAllAvailabilitiesById(int id);
    }
}
