using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
 
    public class SlotModel : BaseModel
    {
        public int Number { get; set; }
        public string Identifier { get; set; }
        public bool IsOccupied { get; set; }
        public int BookingEntityId { get; set; }
        public virtual BookingEntityModel BookingEntity { get; set; }

        public virtual ICollection<AvailabilityModel> Availability { get; set; }
    }
}
