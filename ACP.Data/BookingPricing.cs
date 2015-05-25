﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Data
{
    public class BookingPricing: BaseEntity
    {
        public string Name {get;set;}
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public virtual ICollection<DayPrice> Price { get; set; }
    }
}
