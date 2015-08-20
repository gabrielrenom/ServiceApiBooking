using ACP.Business.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class RootBookingPropertyModel : BaseModel
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public RootBookingPropertyType Type { get; set; }
        public int RootBookingEntityId { get; set; }

        public virtual RootBookingEntityModel RootBookingEntity { get; set; }
    }
}
