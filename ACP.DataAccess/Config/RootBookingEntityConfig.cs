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
        }
    }
}
