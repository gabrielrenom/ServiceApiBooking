using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class PaymentConfig : EntityTypeConfiguration<Payment>
    {
        public PaymentConfig()
        {
            //## Primary Key
            HasKey(t => t.Id);

            HasRequired(p => p.Status)
            .WithMany()
            .HasForeignKey(p => p.StatusId)
            .WillCascadeOnDelete(false);

            HasRequired(t => t.Customer)
            .WithMany(t => t.Payments)
            .HasForeignKey(t => t.CustomerId)
            .WillCascadeOnDelete(false);


            HasRequired(p => p.PaymentMethod)
            .WithMany()
            .HasForeignKey(p => p.PaymentMethodId)
            .WillCascadeOnDelete(false);

            HasRequired(t => t.CreditCard)
          .WithMany(t => t.Payments)
          .HasForeignKey(t => t.CreditCardId)
          .WillCascadeOnDelete(false);

            HasRequired(t => t.BankAccount)
            .WithMany(t => t.Payments)
            .HasForeignKey(t => t.BankAccountId)
            .WillCascadeOnDelete(false);

            HasRequired(t => t.Currency)
            .WithMany(t => t.Payments)
            .HasForeignKey(t => t.CurrencyId)
       .WillCascadeOnDelete(false);
        }
    }
}
