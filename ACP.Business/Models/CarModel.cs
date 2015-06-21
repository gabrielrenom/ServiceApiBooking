using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACP.Business.Models
{
    public class CarModel
    {
        public string Registration { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }

        public int UserId { get; set; }

        public virtual UserModel User { get; set; }
    }
}
