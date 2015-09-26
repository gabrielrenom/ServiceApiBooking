
using ACP.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess.Config
{
    public class CustomerConfig: EntityTypeConfiguration<Customer>
    {
        public CustomerConfig()
        {
            HasKey(t => t.Id);
         
        }
    }
}
