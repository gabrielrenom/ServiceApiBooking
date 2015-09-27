using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
 
    public class User: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int  AddressId{get;set;}        

        public virtual Address Address { get; set; }
       // public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Car> Cars { get; set; }        
    }
}
