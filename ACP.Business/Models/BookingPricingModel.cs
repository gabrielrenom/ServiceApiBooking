using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class BookingPricingModel: BaseModel
    {
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public BookingEntityModel BookingEntity { get; set; }

        public int BookingEntityId { get; set; }
        public virtual ICollection<DayPriceModel> DayPrices { get; set; }        
    }
}
