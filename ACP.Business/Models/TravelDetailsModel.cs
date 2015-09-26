using System;

namespace ACP.Business.Models
{
    public class TravelDetailsModel:BaseModel
    {
        public string OutboundTerminal { get; set; }
        public string ReturnboundTerminal { get; set; }
        public string OutboundFlight { get; set; }
        public string ReturnFlight { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime OutboundDate { get; set; }
    }
}