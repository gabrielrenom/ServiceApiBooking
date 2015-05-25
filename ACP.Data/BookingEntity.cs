using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class BookingEntity: BaseEntity
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public byte[] Logo { get; set; }
        public decimal Price { get; set; }
        public decimal Comission { get; set; }
        public bool Sameday { get; set; }

        public virtual ICollection<BookingPricing> Pricing {get;set;}
        public virtual ICollection<Address> Address { get; set; }
        public virtual ICollection<BookingService> Service { get; set; }
    }
}
