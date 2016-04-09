using System;

namespace Web.Models
{
    public class ReviewView
    {
        public string ClientName { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Review { get; set; }
        public int Rating { get; set; }
    }
}