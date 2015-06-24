using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class ExtraConfig : EntityTypeConfiguration<Extra>
    {
        public ExtraConfig()
        {
            HasKey(x => x.Id);

            HasRequired(x => x.BookingEntity)
            .WithMany(x => x.Extras)
            .HasForeignKey(x => x.BookingEntityId)
            .WillCascadeOnDelete(false);
        }
    }
}   