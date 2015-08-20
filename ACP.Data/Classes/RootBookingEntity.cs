using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class RootBookingEntity: BaseEntity
    {
        public string Name {get;set;}
        public string Telephone { get; set; }
        public int AddressId { get; set; }
        public int StatusId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<BookingEntity> BookingEntities { get; set; }
        public virtual ICollection<RootBookingProperty> Properties { get; set; }
    }
}
