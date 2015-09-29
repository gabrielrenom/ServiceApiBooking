using System.Collections;
using System.Collections.Generic;

namespace ACP.Data
{
    public class Customer: BaseEntity
    {
        public string Title { get; set; }
        public string Initials { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }    
         
        public virtual ICollection<Booking> Bookings {get;set;}
        //public virtual ICollection<Payment> Payments { get; set; }
    }
}