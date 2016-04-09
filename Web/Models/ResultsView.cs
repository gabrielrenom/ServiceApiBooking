using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ResultsView
    {
        public string Company { get; set; }
        public byte[] CompanyLogo { get; set; }
        public int TransferTime { get; set; }
        public int DistanceFromAirport { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public bool IsFamilyFriendly { get; set; }
        public bool IsRegularTransfers { get; set; }
        public bool IsRetainKeys { get; set; }
        public string Summary { get; set; }
        public string FullString { get; set; }
        public string Important { get; set; }
        public List<ReviewView> Reviews {get;set;}

    }
}