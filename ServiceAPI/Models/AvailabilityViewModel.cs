using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAPI.Models
{ 
    public class AvailabilityViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int StatusType { get; set; }
        public int SlotId { get; set; }

        public decimal Price { get; set; }

        public string Airport { get; set; }

        public string AirportCode { get; set; }

        public string CarPark { get; set; }
        public string CarParkCode { get; set; }
    }
}
