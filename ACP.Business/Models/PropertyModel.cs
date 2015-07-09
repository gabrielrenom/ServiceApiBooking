using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Business.Models
{
    public class PropertyModel: BaseModel
    {
        public string Key;
        public string Value;
        public PropertyType Type;
        public int BookingEntityId { get; set; }

        public virtual BookingEntityModel BookingEntity { get; set; }
    }

    public enum PropertyType { 
        String=100,
        Int = 101,
        Decimal=102,
        Bool = 103,        
        DateTime = 104,
        TimeSpan = 105,
        Geolocation = 106,
        Image = 107,
        Document = 108,
        Contact = 109,
    }
}
