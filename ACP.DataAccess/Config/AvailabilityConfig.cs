using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class AvailabilityConfig:EntityTypeConfiguration<Availability>
    {
        public AvailabilityConfig()
        {
            //## Primary Key
           HasKey(t => t.Id);

           HasRequired(t => t.Slot)
               .WithMany(t => t.Availability)
               .HasForeignKey(t => t.SlotId)
               .WillCascadeOnDelete(false);

           HasRequired(p => p.Status)
               .WithMany()
               .HasForeignKey(p => p.StatusId)
               .WillCascadeOnDelete(false);
        }
    }
}
