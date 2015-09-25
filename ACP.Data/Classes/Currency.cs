using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class Currency: BaseEntity
    {
        public string Symbol { get; set; }
        public string Code { get; set; }
        public string CountryCode { get; set; }
    }
}
