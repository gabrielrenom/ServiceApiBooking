using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class CreditCard: BaseEntity
    {
        public string Number { get; set; }
        public string PlainNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool Deleted { get; set; }
        public string GateWayKey { get; set; }
        public string Lock { get; set; }
    }
}
