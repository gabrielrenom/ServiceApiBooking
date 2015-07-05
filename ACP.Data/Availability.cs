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
        public int BookingEntityId { get; set; }

        public virtual Status Status { get; set; }
        public virtual BookingEntity BookingEntity { get; set; }

    }
}
