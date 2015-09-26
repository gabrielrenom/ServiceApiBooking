namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookings_status_added_as_a_flag : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "StatusId", "dbo.Status");
            DropIndex("dbo.Bookings", new[] { "StatusId" });
            AddColumn("dbo.Bookings", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Bookings", "StatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "StatusId", c => c.Int(nullable: false));
            DropColumn("dbo.Bookings", "Status");
            CreateIndex("dbo.Bookings", "StatusId");
            AddForeignKey("dbo.Bookings", "StatusId", "dbo.Status", "Id");
        }
    }
}
