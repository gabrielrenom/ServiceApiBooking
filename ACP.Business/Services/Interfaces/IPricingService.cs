﻿using ACP.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IPricingService
    {
        Task<PriceQuoteModel> GetPrice(QuoteModel model);

    }
}
