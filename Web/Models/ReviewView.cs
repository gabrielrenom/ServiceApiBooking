using System;

namespace Web.Models
{
    public class ReviewView
    {
        public string ClientName { get; set; }
        public string Subject { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Review { get; set; }
        public double Rating { get; set; }
    }
}