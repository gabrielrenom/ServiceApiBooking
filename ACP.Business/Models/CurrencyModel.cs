using System.Collections.Generic;

namespace ACP.Business.Models
{
    public class CurrencyModel: BaseModel
    {
        public string Symbol { get; set; }
        public string Code { get; set; }
        public string CountryCode { get; set; }
        public virtual ICollection<PaymentModel> Payments { get; set; }
    }
}