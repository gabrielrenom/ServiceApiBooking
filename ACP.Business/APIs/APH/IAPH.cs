using ACP.Business.APIs.APH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.APIs.APH
{
    public interface IAPH
    {
        ACP.Business.APIs.APH.Models.Availability.API_Reply CarParkAvailability(API_Request request);

        API_Reply CarPrice(API_Request request);

        API_Reply CarParkInformation(API_Request request);

        bool FillBookingEnties();
    }
}
