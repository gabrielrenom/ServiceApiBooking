using System.Collections.Generic;

namespace ACP.Business.Models
{
    public class BankAccountModel: BaseModel
    {
        public string AbaRouting { get; set; }
        public string BankAccountNumber { get; set; }
        public int Type { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public bool Lock { get; set; }
        public virtual ICollection<PaymentModel> Payments { get; set; }
    }
}