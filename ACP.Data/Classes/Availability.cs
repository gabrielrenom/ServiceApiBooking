using ACP.Data;
using ACP.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class Availability : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public AvailabilityStatus Status { get; set; }
        //public int StatusId { get; set; }
        public int SlotId { get; set; }

        //public virtual Status Status { get; set; }
        public virtual Slot Slot { get; set; }

    }
}
