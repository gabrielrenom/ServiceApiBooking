using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class BookingService : BaseEntity
    {
        public string Name { get; set; }

        public int BookingEntityId { get; set; }

        public virtual BookingEntity BookingEntity { get; set; }      
    }
}
