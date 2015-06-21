using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class UserModel: BaseModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AddressId { get; set; }

        public virtual AddressModel Address { get; set; }
        public virtual ICollection<BookingModel> Bookings { get; set; }
        public virtual ICollection<CarModel> Cars { get; set; }
    }
}
