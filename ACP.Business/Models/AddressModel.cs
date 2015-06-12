using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Business.Models
{
    public class AddressModel: BaseModel
    {
        public int Number { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public string Country { get; set; }        
    }
}
