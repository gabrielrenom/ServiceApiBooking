using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class RootBookingEntityConfig : EntityTypeConfiguration<RootBookingEntity>
    {
        public RootBookingEntityConfig()
        {
            HasKey(t => t.Id);

            HasRequired(p => p.Address)
                .WithMany()
                .HasForeignKey(p => p.AddressId)
                .WillCascadeOnDelete(false);

            HasRequired(p => p.Status)
                .WithMany()
                .HasForeignKey(p => p.StatusId)
                .WillCascadeOnDelete(false);
        }
    }
}
