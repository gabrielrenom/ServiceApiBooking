using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class PropertiesConfig : EntityTypeConfiguration<Property>
    {
        public PropertiesConfig()
        {
            HasKey(t=>t.Id);
       
            HasRequired(t => t.BookingEntity)
                .WithMany(t => t.Properties)
                .HasForeignKey(t => t.BookingEntityId);                 
        }
    }
}
