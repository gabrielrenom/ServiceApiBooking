using System.Collections.Generic;

namespace ACP.Business.Models
{
    public class CustomerModel: BaseModel
    {
        public string Title { get; set; }
        public string Initials { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public int AddressId { get; set; }
        public virtual AddressModel Address { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }

        public virtual ICollection<BookingModel> Bookings { get; set; }
        public virtual ICollection<PaymentModel> Payments { get; set; }
    }
}