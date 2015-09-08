using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAPI.Models
{
    public class RootBookingPropertyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Telephone { get; set; }
        public int AddressId { get; set; }
        public int StatusId { get; set; }
        public virtual AddressViewModel Address { get; set; }
        public virtual StatusViewModel Status { get; set; }
        public virtual ICollection<BookingEntityViewModel> BookingEntities { get; set; }
        public virtual ICollection<RootBookingPropertyViewModel> Properties { get; set; }

        public virtual ICollection<BookingServiceViewModel> BookingServices { get; set; }
        
    }
}
