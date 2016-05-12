using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class QuoteModelView
    {
        [Required]
        public String Airport { get; set; }
        [Required]
        public String DropOffDate { get; set; }
        [Required]
        public String ReturnDate { get; set; }
        public String Discount { get; set; }
    }
}