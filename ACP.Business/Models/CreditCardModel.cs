using System;
using System.Collections.Generic;

namespace ACP.Business.Models
{
    public class CreditCardModel: BaseModel
    {
        public string Number { get; set; }
        public string PlainNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public bool Deleted { get; set; }
        public string GateWayKey { get; set; }
        public bool Lock { get; set; }

        public virtual ICollection<PaymentModel> Payments { get; set; }
    }
}