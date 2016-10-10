using ACP.Business.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Services.Interfaces
{
    public interface IPayPalService
    {
        string PaymentWithCreditCard(PayPalModel model, APIContext context);
    }
}
