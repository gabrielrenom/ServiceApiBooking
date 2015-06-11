using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Business.Models
{
    public class BookingItemModel
    {
        public string Description { get; set; }
        public List<string> Features { get; set; }
        public string Directions { get; set; }
        public string Arrival { get; set; }
        public string Departure { get; set; }
        public string Information { get; set; }
        public string Transfers { get; set; }
        public InsuranceTypeModel Insurance { get; set; }
        public string Notes { get; set; }
        public string AditionalInformation { get; set; }
        public PriceModel Price { get; set; }
    }
}
