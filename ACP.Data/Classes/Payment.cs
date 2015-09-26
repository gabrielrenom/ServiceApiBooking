using ACP.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.Data
{
    public class Payment: BaseEntity
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int CreditCardId { get; set; }
        public int CurrencyId { get; set; }
        public int BankAccountId { get; set; }

        public StatusType Status { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Customer Customer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public virtual CreditCard CreditCard { get; set; }
        public virtual Currency Currency {get;set;}
        public virtual BankAccount BankAccount { get; set; }
    }
}
