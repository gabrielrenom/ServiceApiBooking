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
        UserModel Add(UserModel user);
        
        IList<UserModel> GetAll();

        UserModel GetById(int Id);

        bool Update(UserModel model);

        bool DeleteById(int Id);
    }

     
}
