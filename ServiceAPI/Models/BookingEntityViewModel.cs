using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAPI.Models
{
    public class BookingEntityViewModel
    {
        public string Name { get; set; }

        public string Code { get; set; }
        public byte[] Image { get; set; }
        public byte[] Logo { get; set; }
        public decimal Price { get; set; }
        public decimal Comission { get; set; }
        public bool Sameday { get; set; }
        public int RootBookEntityId { get; set; }
        public EntityViewType EntityType { get; set; }

        public virtual ICollection<PropertyViewModel> Properties { get; set; }

        public virtual RootBookingPropertyViewModel Airport { get; set; }
        public virtual ICollection<ExtraViewModel> Extras { get; set; }
        public virtual ICollection<BookingPricingViewModel> Prices { get; set; }
        public virtual AddressViewModel Address { get; set; }
        public virtual ICollection<BookingServiceViewModel> Service { get; set; }
        public virtual ICollection<AvailabilityViewModel> Availability { get; set; }
    }
}
