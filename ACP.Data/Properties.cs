﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class Properties :  BaseEntity
    {    
        public string Key;
        public object Value;
        public PropertyType Type;
        public int BookingEntityId { get; set; }

        public virtual BookingEntity BookingEntity { get; set; }
    }

    public enum PropertyType
    {
        String = 100,
        Int = 101,
        Decimal = 102,
        Bool = 103,
        DateTime = 104,
        TimeSpan = 105,
        Geolocation = 106,
        Image = 107,
        Document = 108,
        Contact = 109,
    }
}
