using ACP.Data;
using ACP.DataAccess.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACP.DataAccess
{
    public class ACPContext: DbContext
    {
        public ACPContext()
            : base("ACP")
        { 
        }

        static ACPContext()
        {
            Database.SetInitializer<ACPContext>(new ACPInitialiser());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ServiceConfig());            

        }

        public DbSet<Service> Instances { get; set; }        
    }
}
