﻿using System;
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

        [Required(ErrorMessage = "The model is required")]
        public string Model { get; set; }

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
        public DateTime ReturnDate { get; internal set; }
        public int AirportId { get; internal set; }
        public decimal BookingFee { get; internal set; }
    }
}