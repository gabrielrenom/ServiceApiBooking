using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ACP.Business.Models
{
    public class CustomerModel: BaseModel
    {
        public string Title { get; set; }
        public string Initials { get; set; }
        [Required]
        public string Forename { get; set; }
        [Required]
        public string Surname { get; set; }
        public int AddressId { get; set; }
        public virtual AddressModel Address { get; set; }
        public string Telephone { get; set; }
        [Required]
        public string Mobile { get; set; }
        public string Fax { get; set; }
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        public virtual ICollection<BookingModel> Bookings { get; set; }
        //public virtual ICollection<PaymentModel> Payments { get; set; }
    }
}