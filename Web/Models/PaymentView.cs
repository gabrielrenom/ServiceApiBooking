using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PaymentView
    {
        public string Name { get; set; }
        public string Currency { get; set; }
        public string Price { get; set; }
        public int Quantity { get; set; }

        public string SKU { get; set; }
        public string BillingAddressCity { get; set; }
        public string BillingAddressCountry { get; set; }
        public string BillingAddressLine1 { get; set; }
        public string BillingAddressPostCode { get; set; }
        public string BillingAddressState{ get; set; }

        public string CVV2 { get; set; }
        public string ExpireMonth { get; set; }
        public string ExpireYear { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }

        public string Description { get; set; }
        public string InvoiceNumber { get; set; }
        public string Total { get; set; }
    }
}