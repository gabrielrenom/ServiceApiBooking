using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class BookingLink: BaseEntity
    {
        public int BookingId {get;set;}
        public int SlotId { get; set; }
        public int AvailabilityId { get; set; }

        public virtual Booking Booking { get; set; }
        public virtual Slot Slot { get; set; }
        public virtual Availability Availability { get; set; }
    }
}
