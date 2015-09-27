using ACP.Business.Enums;
using System.Collections.Generic;

namespace ACP.Business.Models
{
    public class PaymentModel: BaseModel
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int? CreditCardId { get; set; }
        public int CurrencyId { get; set; }
        public int? BankAccountId { get; set; }

        public virtual StatusType Status { get; set; }
        public virtual BookingModel Booking { get; set; }
        public virtual CustomerModel Customer { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public virtual CreditCardModel CreditCard { get; set; }
        public virtual CurrencyModel Currency { get; set; }
        public virtual BankAccountModel BankAccount { get; set; }
    }
}