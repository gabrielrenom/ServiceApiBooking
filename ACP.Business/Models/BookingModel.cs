using ACP.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class BookingModel:BaseModel
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

        public virtual ICollection<ExtraModel> Extras { get; set; }

        public virtual StatusType Status { get; set; }
        public virtual UserModel User { get; set; }
        public virtual CustomerModel Customer { get; set; }
        public virtual ICollection<PaymentModel> Payments { get; set; }
        public virtual TravelDetailsModel TravelDetails { get; set; }
        public virtual CarModel Car { get; set; }

        public virtual ICollection<BookingLinkModel> BookingLink { get; set; }
      
    }
}
