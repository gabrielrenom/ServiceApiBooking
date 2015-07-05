namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class properties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingEntities", "EntityType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookingEntities", "EntityType");
        }
    }
}
