using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> Add(UserModel user);
        
        Task<IList<UserModel>> GetAll();

        Task<UserModel> GetById(int Id);

        Task<bool> Update(UserModel model);

        Task<bool> DeleteById(int Id);
    }

     
}
