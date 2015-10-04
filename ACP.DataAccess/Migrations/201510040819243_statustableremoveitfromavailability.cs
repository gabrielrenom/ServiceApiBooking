namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statustableremoveitfromavailability : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Availabilities", "StatusId", "dbo.Status");
            DropIndex("dbo.Availabilities", new[] { "StatusId" });
            AddColumn("dbo.Availabilities", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Availabilities", "StatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Availabilities", "StatusId", c => c.Int(nullable: false));
            DropColumn("dbo.Availabilities", "Status");
            CreateIndex("dbo.Availabilities", "StatusId");
            AddForeignKey("dbo.Availabilities", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
        }
    }
}
