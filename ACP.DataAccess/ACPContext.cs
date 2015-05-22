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

            //modelBuilder.Configurations.Add(new FeatureInstanceConfig());
            //modelBuilder.Configurations.Add(new AssetConfig());
            //modelBuilder.Configurations.Add(new AssetTypeConfig());
            //modelBuilder.Configurations.Add(new CategoryConfig());
            //modelBuilder.Configurations.Add(new AssetCategoryConfig());
            //modelBuilder.Configurations.Add(new MetadataDefinitionConfig());
            //modelBuilder.Configurations.Add(new MetadataDefinitionItemConfig());
            //modelBuilder.Configurations.Add(new MetadataItemConfig());
            //modelBuilder.Configurations.Add(new RoleConfig());
            //modelBuilder.Configurations.Add(new ScenarioConfig());
            //modelBuilder.Configurations.Add(new ScenarioRangeConfig());

        }

        //public DbSet<AmtInstance> Instances { get; set; }
        //public DbSet<Asset> Assets { get; set; }
        //public DbSet<AssetType> AssetTypes { get; set; }
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<AssetCategory> AssetCategories { get; set; }
        //public DbSet<MetadataDataType> MetadataDataTypes { get; set; }
        //public DbSet<MetadataDefinition> MetadataDefinitions { get; set; }
        //public DbSet<MetadataDefinitionItem> MetadataDefinitionItems { get; set; }
        //public DbSet<MetadataItem> MetadataItems { get; set; }
        //public DbSet<Scenario> Scenarios { get; set; }
        //public DbSet<ScenarioRange> ScenarioRanges { get; set; }
        //public DbSet<PermissionType> PermissionTypes { get; set; }
        //public DbSet<AmtRole> Roles { get; set; }
        //public DbSet<RolePermission> RolePermissions { get; set; }
        //public DbSet<UserRole> UserRoles { get; set; }
    }
}
