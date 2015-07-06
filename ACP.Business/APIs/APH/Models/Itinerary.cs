using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.APIs.APH.Models
{
    public class Itinerary
    {
        public string ArrivalDate { get; set; }
        public string DepartDate { get; set; }
        public string ArrivalTime { get; set; }
        public string DepartTime { get; set; }
        public string Location { get; set; }
        public string Terminals { get; set; }
        public string CarParkCode { get; set; }
        public string NumberOfPax { get; set; }
    }
}
