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
        public int PaymentMethodId { get; set; }
        public int CreditCardId { get; set; }
        public int CurrencyId { get; set; }
        public int StatusId { get; set; }

        public virtual Status Status { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual CreditCard CreditCard { get; set; }
        public virtual Currency Currency {get;set;}
        public virtual BankAccount BankAccount { get; set; }
    }
}
