using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class BookingGuestViewModel
    {

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ConfirmEmail { get; set; }

        [Required()]
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required]
        public string Mobile { get; set; }

        [Range(1, 8)]
        [Required]
        public int Passangers { get; set; }

        [Required]
        public string Registration { get; set; }

        [Required]
        public string Make { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Color { get; set; }
        public string TerminalIn { get; set; }

        [Required]
        public string OutboundFlight { get; set; }
        public string TerminalOut { get; set; }

        [Required]
        public string InboundFlight { get; set; }
        public string CouponDiscount { get; set; }
        public bool IsAddSMSConfirmation { get; set; }
        public bool IsCancelationCover { get; set; }
        public bool IsAddCarWashService { get; set; }

    }
}