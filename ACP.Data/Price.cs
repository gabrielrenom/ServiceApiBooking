using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Data
{
    public class DayPrice : BaseEntity
    {
        public int Day { get; set; }
        public decimal Dayprice { get; set; }

        public ICollection<HourPrice> HourPrice { get; set; }
    }
}
