using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class BookingPricingConfig : EntityTypeConfiguration<BookingPricing>
    {
        public BookingPricingConfig()
        {
            //## Primary Key
            HasKey(t => t.Id);

            //add contraint
            HasRequired(p => p.BookingEntity)
                .WithMany(p => p.Prices)
                .HasForeignKey(t => t.BookingEntityId)
                .WillCascadeOnDelete(false);
        }
    }
}
