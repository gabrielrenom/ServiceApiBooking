using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data.Classes
{
    public class Slot: BaseEntity
    {
        public int Number { get; set; }
        public string Identifier { get; set; }
        public bool IsOccupied { get; set; }
        public int BookingEntityId { get; set; }
        public virtual BookingEntity BookingEntity { get; set; }

        public virtual ICollection<Availability> Availability { get; set; }
    }
}
