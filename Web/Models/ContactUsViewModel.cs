using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ContactUsViewModel
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Comments")]
        public string Comments { get; set; }
        public string Confirmation { get; internal set; }
    }
}