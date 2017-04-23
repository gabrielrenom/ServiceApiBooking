using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class BookingEntityConfig : EntityTypeConfiguration<BookingEntity>
    {
        public BookingEntityConfig()
        {
            //## Primary Key
            HasKey(t => t.Id);
            
            HasRequired(a => a.Address)
            .WithMany()
            .HasForeignKey(u => u.AddressId);

            HasMany(p => p.Properties)
            .WithRequired()
            .HasForeignKey(c => c.BookingEntityId)
            .WillCascadeOnDelete(false);

            HasMany(p => p.Extras)
            .WithRequired()
            .HasForeignKey(c => c.BookingEntityId)
            .WillCascadeOnDelete(false);

            HasMany(p => p.Prices)
           .WithRequired()
           .HasForeignKey(c => c.BookingEntityId)
           .WillCascadeOnDelete(false);

            HasMany(p => p.Service)
           .WithRequired()
           .HasForeignKey(c => c.BookingEntityId)
           .WillCascadeOnDelete(false);

            HasMany(p => p.Slot)
          .WithRequired()
          .HasForeignKey(c => c.BookingEntityId)
          .WillCascadeOnDelete(false);

            HasMany(p => p.Reviews)
        .WithRequired()
        .HasForeignKey(c => c.BookingEntityId)
        .WillCascadeOnDelete(false);


            //HasRequired(u => u.RootBookingEntity).WithRequiredPrincipal().WillCascadeOnDelete(true);
            //// HasRequired(u => u.Address).WithRequiredPrincipal().WillCascadeOnDelete(true);

            //HasMany(u => u.Properties).WithRequired().HasForeignKey(a => a.BookingEntityId).WillCascadeOnDelete(true);
            //HasMany(u => u.Extras).WithRequired().HasForeignKey(a => a.BookingEntityId).WillCascadeOnDelete(true);
            //HasMany(u => u.Prices).WithRequired().HasForeignKey(a => a.BookingEntityId).WillCascadeOnDelete(true);
            //HasMany(u => u.Service).WithRequired().HasForeignKey(a => a.BookingEntityId).WillCascadeOnDelete(true);
            //HasMany(u => u.Slot).WithRequired().HasForeignKey(a => a.BookingEntityId).WillCascadeOnDelete(true);
            //HasMany(u => u.Reviews).WithRequired().HasForeignKey(a => a.BookingEntityId).WillCascadeOnDelete(true);

            //HasRequired(p => p.RootBookingEntity)
            // .WithMany(p=>p.BookingEntities)
            // .HasForeignKey(p => p.RootBookingEntityId)
            // .WillCascadeOnDelete(false);


        }
    }
}
