using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Data
{
    public class Extra:BaseEntity
    {
        public string Name{get;set;}
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int BookingEntityId { get; set; }

        public virtual BookingEntity BookingEntity { get; set; }
    }
}
