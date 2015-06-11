using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Business.Models
{
    public class LocationModel
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public AddressModel Address { get; set; }
    }
}
