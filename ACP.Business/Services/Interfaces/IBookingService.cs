﻿using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IBookingService
    {
        Task<BookingModel> Add(BookingModel model);
        Task<bool> Remove(int Id);
        Task<bool> Update(BookingModel model);
        Task<BookingModel> GetById(int Id);
        Task<IList<BookingModel>> GetAll();        
    }
}