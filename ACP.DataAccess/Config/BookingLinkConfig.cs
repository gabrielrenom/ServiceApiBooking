using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class BookingLinkConfig: EntityTypeConfiguration<BookingLink>
    {
        public BookingLinkConfig()
        {
            //## Primary Key
            HasKey(t => t.Id);

           HasRequired(t => t.Booking)
          .WithMany(t => t.BookingLink)
          .HasForeignKey(t => t.BookingId)
          .WillCascadeOnDelete(false);

           HasRequired(t => t.Slot)
            .WithMany(t => t.BookingLink)
         .HasForeignKey(t => t.SlotId)
         .WillCascadeOnDelete(false);

            HasRequired(t => t.Availability)
          .WithMany()
          .HasForeignKey(t => t.AvailabilityId)
          .WillCascadeOnDelete(false);
        }
    }
}
