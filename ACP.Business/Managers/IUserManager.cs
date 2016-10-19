using ACP.Business.Models;
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Managers
{
    public interface IUserManager: IBaseACPManager<UserModel,User>
    {
        IList<UserModel> GetAllUsersWithCarsAndBookings();

        IList<UserModel> GetAllUsersWithCars();

        Task<bool> Login(string username, string password);
        Task<UserModel> GetUserDetails(string username, string password);
    }
}
