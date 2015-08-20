using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class RootBookingPropertiesConfig : EntityTypeConfiguration<RootBookingProperty>
    {
        public RootBookingPropertiesConfig()
        {
            HasKey(t => t.Id);

            HasRequired(t => t.RootBookingEntity)
                .WithMany(t => t.Properties)
                .HasForeignKey(t => t.RootBookingEntityId);
        }
    }
}
