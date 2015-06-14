using ACP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{    
    public class RootBookingEntityModel : BaseModel
    {
        public string Name { get; set; }
        public string Telephone { get; set; }
        public int AddressId { get; set; }
        public int StatusId { get; set; }

        public virtual AddressModel Address { get; set; }
        public virtual StatusModel Status { get; set; }
        public virtual ICollection<BookingEntityModel> BookingEntities { get; set; }
    }
}
