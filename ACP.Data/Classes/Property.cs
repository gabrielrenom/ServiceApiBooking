using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class Property :  BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public PropertyType Type { get; set; }
        public int BookingEntityId { get; set; }

        public virtual BookingEntity BookingEntity { get; set; }
    }

}
