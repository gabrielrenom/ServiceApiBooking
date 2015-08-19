
using ACP.Business.APIs.PP.Models.Airports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.APIs.PP
{
    public interface IPurpleParking
    {
        Task<ACP.Business.APIs.PP.Models.Airports.response> GetAirports();
        List<responseAirportCarPark> GetCarParks();
    }
}
