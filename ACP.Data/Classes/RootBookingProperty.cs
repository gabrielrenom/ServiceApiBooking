using ACP.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class RootBookingProperty:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public RootBookingPropertyType Type { get; set; }
        public int RootBookingEntityId { get; set; }

        public virtual RootBookingEntity RootBookingEntity { get; set; }
    }
    
}
