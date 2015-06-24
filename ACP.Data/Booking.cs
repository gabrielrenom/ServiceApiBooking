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
        public int StatusId { get; set; }
        public int UserId { get; set; }
        public int BookingEntityId { get; set; }

        
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual BookingEntity Entity { get; set; }
    }
}
