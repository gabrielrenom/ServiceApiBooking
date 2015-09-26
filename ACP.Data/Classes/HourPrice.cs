using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Data
{
    public class HourPrice: BaseEntity
    {
        public TimeSpan HourMinute { get; set; }
        public decimal Hourprice { get; set; }
        public int DayPriceId { get; set; }
        
        public virtual DayPrice DayPrice { get; set; }
    }
}
