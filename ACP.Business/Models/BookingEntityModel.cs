using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class BookingEntityModel: BaseModel
    {
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public byte[] Logo { get; set; }
        public decimal Price { get; set; }
        public decimal Comission { get; set; }
        public bool Sameday { get; set; }

        //public virtual RootBookingEntity RootBookingEntity { get; set; }
        //public virtual ICollection<BookingPricing> Prices { get; set; }
        //public virtual Address Address { get; set; }
        //public virtual ICollection<BookingService> Service { get; set; }
    }
}
