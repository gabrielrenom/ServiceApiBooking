using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class BookingLoginViewModel
    {

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "The title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The first name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The last name is required")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "The mobile number is required")]
        public string Mobile { get; set; }

        [Range(1, 8)]
        [Required(ErrorMessage = "Passamger numbers are required")]
        public int Passangers { get; set; }

        [Required(ErrorMessage = "The registration plate is required")]
        public string Registration { get; set; }

        [Required(ErrorMessage = "The make is required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "The car model is required")]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "The colour is required")]
        public string Color { get; set; }
        public string TerminalIn { get; set; }

        [Required(ErrorMessage = "The outbound fight is required")]
        public string OutboundFlight { get; set; }
        public string TerminalOut { get; set; }

        [Required(ErrorMessage = "The inbound fight is required")]
        public string InboundFlight { get; set; }

        public string CouponDiscount { get; set; }
        public bool IsAddSMSConfirmation { get; set; }
        public bool IsCancelationCover { get; set; }
        public bool IsAddCarWashService { get; set; }

        public decimal Price { get; set; }
        public DateTime DropOffDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int AirportId { get; set; }
        public decimal BookingFee { get; set; }

        [DataType(DataType.CreditCard)]
        [Required(ErrorMessage = "The Credit Number required")]
        public string CardNumber { get; set; }

        [Required(ErrorMessage = "The Credit Nameis required")]
        public string CardName { get; set; }

        [Required(ErrorMessage = "The Credit Card Type is required")]
        public string CardType { get; set; }

        [Required(ErrorMessage = "The Epiry Month is required")]
        public string Expirymonth { get; set; }

        [Required(ErrorMessage = "The Epiry Year is required")]
        public string ExpiryYear { get; set; }

    
        public int BookEntityId { get; set; }
    }

    public class BookingGuestViewModel
    {
        public BookingGuestViewModel()
        {
            IsUser = false;
        }


        //[Required]
        //[Display(Name = "Email")]
        [EmailAddress]
        public string UserEmail { get; set; }

        //[Required]
        //[DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string ConfirmEmail { get; set; }

        [Required(ErrorMessage = "The title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "The first name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The last name is required")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "The mobile number is required")]
        public string Mobile { get; set; }

        [Range(1, 8)]
        [Required(ErrorMessage = "Passamger numbers are required")]
        public int Passangers { get; set; }

        [Required(ErrorMessage = "The registration plate is required")]
        public string Registration { get; set; }

        [Required(ErrorMessage = "The make is required")]
        public string Make { get; set; }

        [Required(ErrorMessage = "The car model is required")]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "The colour is required")]
        public string Color { get; set; }
        public string TerminalIn { get; set; }

        [Required(ErrorMessage = "The outbound fight is required")]
        public string OutboundFlight { get; set; }
        public string TerminalOut { get; set; }

        [Required(ErrorMessage = "The inbound fight is required")]
        public string InboundFlight { get; set; }

        public string CouponDiscount { get; set; }
        public bool IsAddSMSConfirmation { get; set; }
        public bool IsCancelationCover { get; set; }
        public bool IsAddCarWashService { get; set; }

        public decimal Price { get; set; }
        public DateTime DropOffDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public int AirportId { get; set; }
        public decimal BookingFee { get; set; }

        public int BookEntityId { get; set; }
        public string Description { get; set; }
        public string CarParkName { get; set; }

        //[DataType(DataType.CreditCard)]
        //[Required(ErrorMessage = "The Credit Number required")]
        //public string CardNumber { get; set; }

        //[Required(ErrorMessage = "The Credit Nameis required")]
        //public string CardName { get; set; }

        //[Required]
        //[RegularExpression("^[0-9]*$",ErrorMessage = "The Credit Card Type is required")]
        //public string CreditCardType { get; set; }

        //[Range(1, 12)]
        //[Required(ErrorMessage = "The Expiry Month is required")]
        //public string ExpiryMonth { get; set; }

        //[Range(2016,2030)]
        //[Required(ErrorMessage = "The Expiry Year is required")]
        //public string ExpiryYear { get; set; }

        //[Required(ErrorMessage = "CVV is required")]
        //[MaxLength(3)]
        //[MinLength(3)]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "CVV is required")]
        //public string CVV { get; set; }

        [Required(ErrorMessage = "Please enter Postcode")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Please enter address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter city")]
        public string City { get; set; }
        public string Error { get; set; }
        public int ErrorType { get; set; }

        public bool IsUser { get; set; }
    }

    public class BookingConfirmationView
    {
        public string BookingReference { get; set; }
        public decimal Price { get; set; }
        public DateTime DropOffDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string Description { get; set; }
        public string CarParkName { get; set; }
        public bool IsAddSMSConfirmation { get; set; }
        public bool IsCancelationCover { get; set; }
        public bool IsAddCarWashService { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mobile { get; set; }
        public int Passangers { get; set; }
        public string Registration { get; set; }
        public string Make { get; set; }
        public string CarModel { get; set; }
        public string Color { get; set; }
        public string TerminalIn { get; set; }
        public string OutboundFlight { get; set; }
        public string TerminalOut { get; set; }
        public string InboundFlight { get; set; }
        public string Email { get; set; }
    }
}

