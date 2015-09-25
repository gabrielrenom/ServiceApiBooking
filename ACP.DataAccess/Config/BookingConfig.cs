using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class BookingConfig : EntityTypeConfiguration<Booking>
    {
        public BookingConfig()
        {
            //## Primary Key
            HasKey(t => t.Id);

            HasRequired(t => t.Customer)
            .WithMany(t => t.Bookings)
            .HasForeignKey(t => t.CustomerId)
            .WillCascadeOnDelete(false);

            HasRequired(t => t.User)
            .WithMany(t => t.Bookings)
            .HasForeignKey(t => t.UserId)
            .WillCascadeOnDelete(false);

            HasRequired(t => t.Payment)
            .WithMany(t => t.Bookings)
            .HasForeignKey(t => t.PaymentId)
            .WillCascadeOnDelete(false);


            HasRequired(p => p.Status)
            .WithMany()
            .HasForeignKey(p => p.StatusId)
            .WillCascadeOnDelete(false);
        }
    }
}
