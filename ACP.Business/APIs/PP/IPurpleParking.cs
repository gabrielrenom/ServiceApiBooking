
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
        Task<List<ACP.Business.APIs.PP.Models.Airports.responseAirport>> GetAirports();
        Task<List<ACP.Business.APIs.PP.Models.Airports.responseAirportCarPark>> GetCarParks();
        Task<bool> FillAirports();
    }
}
