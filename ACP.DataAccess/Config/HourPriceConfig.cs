using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class HourPriceConfig : EntityTypeConfiguration<HourPrice>
    {
        public HourPriceConfig()
        {
            //## Primary Key
            HasKey(t => t.Id);           
        }
    }
}
