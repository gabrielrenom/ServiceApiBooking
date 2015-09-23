using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class Availability: BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }
        public int SlotId { get; set; }

        public virtual Status Status { get; set; }
        public virtual Slot Slot { get; set; }

        public virtual BookingLink BookingLink { get; set; }
    }
}
