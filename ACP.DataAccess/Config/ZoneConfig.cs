
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class SlotConfig: EntityTypeConfiguration<Slot>
    {
        public SlotConfig()
        {
            HasKey(t => t.Id);

            HasRequired(t => t.BookingEntity)
                .WithMany(t => t.Slot)
                .HasForeignKey(t => t.BookingEntityId)
                .WillCascadeOnDelete(false);        
        }
    }
}
