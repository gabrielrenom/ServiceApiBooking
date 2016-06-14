using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ResultsView
    {
        public int Id { get; set; }
        public string Company { get; set; }
        public byte[] CompanyLogo { get; set; }
        public decimal? TransferTime { get; set; }
        public decimal? DistanceFromAirport { get; set; }        
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsFamilyFriendly { get; set; }
        public bool IsRegularTransfers { get; set; }
        public bool IsRetainKeys { get; set; }
        public bool Is24hSecurity { get; set; }
        public string Summary { get; set; }
        public string FullString { get; set; }
        public string Important { get; set; }
        public bool IsParkAndRide { get; set; }
        public bool IsMeetAndGreet { get; set; }
        public bool IsOnAirport { get; set; }
        public virtual List<ReviewView> Reviews {get;set;}
        public virtual AddressView Address { get; set; }
        public virtual QuoteModelView Quote { get; set; }
    }
}