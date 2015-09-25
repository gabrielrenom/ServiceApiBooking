using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class TravelDetails: BaseEntity
    {
        public string OutboundTerminal { get; set; }
        public string ReturnboundTerminal { get; set; }
        public string OutboundFlight { get; set; }
        public string ReturnFlight { get; set; }
        public DateTime ReturnDate { get; set; }
        public DateTime OutboundDate { get; set; }
    }
}
