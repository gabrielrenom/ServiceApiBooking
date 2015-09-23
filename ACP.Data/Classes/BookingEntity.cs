using ACP.Data.Classes;
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
        public int AddressId { get; set; }
        public int RootBookingEntityId { get; set; }
        public EntityType EntityType { get; set; }

        public virtual ICollection<Property> Properties { get; set; }
        public virtual ICollection<Extra> Extras { get; set; }
        public virtual RootBookingEntity RootBookingEntity { get; set; }
        public virtual ICollection<BookingPricing> Prices {get;set;}
        public virtual Address Address { get; set; }
        public virtual ICollection<BookingService> Service { get; set; }
        public virtual ICollection<Slot> Slot { get; set; }
        
    }
}
