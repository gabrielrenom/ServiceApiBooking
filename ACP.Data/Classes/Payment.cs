using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class Payment: BaseEntity
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int PaymentMethodId { get; set; }

        public virtual Booking Boooking { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
