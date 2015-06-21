using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class CarConfig: EntityTypeConfiguration<Car>
    {
        public CarConfig()
        {
            HasRequired(p => p.User)
                .WithMany(p => p.Cars)
                .HasForeignKey(p => p.UserId)
                .WillCascadeOnDelete(false);
        }
        
    }
}
