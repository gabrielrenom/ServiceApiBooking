﻿using ACP.Business.Managers;
using ACP.Business.Models;
using ACP.Business.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services
{
    public class UserService: IUserService
    {
        private IUserManager _usermanager;

        public UserService(IUserManager usermanager)
        {
            _usermanager = usermanager;
        }

        public UserModel Add(UserModel user)
        {
            return _usermanager.Add(user);
        }
    

        public IList<UserModel>GetAll()
        {
            return _usermanager.GetAllUsersWithCarsAndBookings().ToList();
        }
}
}
