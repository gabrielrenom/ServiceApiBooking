using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Business.Models
{
    public class HourPriceModel: BaseModel
    {
        public DateTime HourMinute;
        public decimal Hourprice;
        public int DayPriceId { get; set; }

        public DayPriceModel DayPrice { get; set; }
    }
}
