using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class BookingServiceModel : BaseModel
    {
        [Required]
        public string Name { get; set; }
    }
}
