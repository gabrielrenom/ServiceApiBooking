using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Business.Models
{
    public class DayPriceModel: BaseModel
    {
        public int Day { get; set; }
        public decimal Dayprice { get; set; }
        public int BookingPricingId { get; set; }

        public virtual BookingPricingModel BookingPricing { get; set; }
        public ICollection<HourPriceModel> HourPrices { get; set; }
    }
}
