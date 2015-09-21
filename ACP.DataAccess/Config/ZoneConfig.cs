using ACP.Data.Classes;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class ZoneConfig: EntityTypeConfiguration<Zone>
    {
        public ZoneConfig()
        {
            HasKey(t => t.Id);

            HasRequired(t => t.BookingEntity)
                .WithMany(t => t.Zone)
                .HasForeignKey(t => t.BookingEntityId)
                .WillCascadeOnDelete(false);        
        }
    }
}
