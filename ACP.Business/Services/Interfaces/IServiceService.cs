using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IBookingServiceService
    {
        BookingServiceModel Add(BookingServiceModel service);

        Task<BookingServiceModel> AddAsync(BookingServiceModel service);

        BookingServiceModel GetServiceByName(string servicename);

        Task<BookingServiceModel> GetServiceByNameAsync(string servicename);

        bool UpdateService(BookingServiceModel model);

        Task<bool> UpdateServiceAsync(BookingServiceModel model);

        bool RemoveService(int id);

        Task<bool> RemoveServiceAsync(int id);

        IList<BookingServiceModel> GetAll();

        Task<IEnumerable<BookingServiceModel>> GetAllAsync();
    }
}
