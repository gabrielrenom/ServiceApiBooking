using ACP.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class Booking: BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }      
        public int UserId { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }        
        public int TravelDetailsId { get; set; }
        
        public string SourceCode { get; set; }
        public string AgentReference { get; set; }
        public string BookingReference { get; set; }
        public double Cost { get; set; }
        public string Message { get; set; }

        public virtual ICollection<Extra> Extras { get; set; } 

        public StatusType Status { get; set; }
        public virtual User User { get; set; }      
        public virtual Customer Customer { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual TravelDetails TravelDetails { get; set; }
        public virtual Car Car { get; set; }

        public virtual ICollection<BookingLink> BookingLink { get; set; }
    }
}
