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

            HasRequired(p => p.RootBookingEntity)
             .WithMany(p=>p.BookingEntities)
             .HasForeignKey(p => p.RootBookingEntityId)
             .WillCascadeOnDelete(false);
            

        }
    }
}
