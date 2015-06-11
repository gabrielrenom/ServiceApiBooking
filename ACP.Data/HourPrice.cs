using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Data
{
    public class HourPrice: BaseEntity
    {
        public DateTime HourMinute;
        public decimal Hourprice;

        public DayPrice DayPrice { get; set; }
    }
}
