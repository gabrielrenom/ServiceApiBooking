using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data.Classes
{
    public class Review: BaseEntity
    {
        public string Author { get; set; }
        public double Rating { get; set; }
        public string Comments { get; set; }
        public string Subject { get; set; }
        public int BookingEntityId { get; set; }
        public virtual BookingEntity BookingEntity { get; set; }
    }
}
