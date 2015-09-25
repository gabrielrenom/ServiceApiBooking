namespace ACP.Data
{
    public class BankAccount: BaseEntity
    {
        public string AbaRouting { get; set; }
        public string BankAccountNumber { get; set; }
        public int Type { get; set; }
        public string BankName { get; set; }
        public string AccountName { get; set; }
        public bool Lock { get; set; }
    }
}