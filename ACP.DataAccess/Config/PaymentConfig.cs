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

            HasRequired(t => t.Customer)
            .WithMany(t => t.Payments)
            .HasForeignKey(t => t.CustomerId)
            .WillCascadeOnDelete(false);

            HasRequired(t => t.Booking)
            .WithMany(t => t.Payments)
            .HasForeignKey(t => t.BookingId)
            .WillCascadeOnDelete(false);

          //  HasRequired(t => t.CreditCard)
          //.WithMany(t => t.Payments)
          //.HasForeignKey(t => t.CreditCardId)
          //.WillCascadeOnDelete(false);

            this.HasOptional(s => s.CreditCard)
            .WithMany(s => s.Payments)
            .HasForeignKey(s => s.CreditCardId);
            
            //HasRequired(t => t.BankAccount)
            //.WithMany(t => t.Payments)
            //.HasForeignKey(t => t.BankAccountId)
            //.WillCascadeOnDelete(false);

            this.HasOptional(s => s.BankAccount)
            .WithMany(s => s.Payments)
            .HasForeignKey(s => s.BankAccountId);
            
            HasRequired(t => t.Currency)
            .WithMany(t => t.Payments)
            .HasForeignKey(t => t.CurrencyId)
       .WillCascadeOnDelete(false);
        }
    }
}
