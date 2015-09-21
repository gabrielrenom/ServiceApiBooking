namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_zone_field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zones", "Number", c => c.Int(nullable: false));
            AddColumn("dbo.Zones", "Identifier", c => c.String());
            AddColumn("dbo.Zones", "IsOccupied", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Zones", "IsOccupied");
            DropColumn("dbo.Zones", "Identifier");
            DropColumn("dbo.Zones", "Number");
        }
    }
}
