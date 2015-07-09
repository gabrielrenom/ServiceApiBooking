namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class properties_key_value : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Properties", "Key", c => c.String());
            AddColumn("dbo.Properties", "Value", c => c.String());
            AddColumn("dbo.Properties", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Properties", "Type");
            DropColumn("dbo.Properties", "Value");
            DropColumn("dbo.Properties", "Key");
        }
    }
}
