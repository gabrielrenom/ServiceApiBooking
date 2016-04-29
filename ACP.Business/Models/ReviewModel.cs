using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class ReviewModel: BaseModel
    {

        public string Author { get; set; }
        public double Rating { get; set; }
        public string Comments { get; set; }
        public string Subject { get; set; }
        public int BookingEntityId { get; set; }
        public virtual BookingEntityModel BookingEntity { get; set; }
    }
}
