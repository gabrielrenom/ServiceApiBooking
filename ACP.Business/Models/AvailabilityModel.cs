using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public  class AvailabilityModel: BaseModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int StatusId { get; set; }
        public int SlotId { get; set; }

        public virtual StatusModel Status { get; set; }
        public virtual SlotModel Slot { get; set; }

    }
}
