namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class payment_status_changed : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "StatusId", "dbo.Status");
            DropIndex("dbo.Payments", new[] { "StatusId" });
            AddColumn("dbo.Payments", "Status", c => c.Int(nullable: false));
            DropColumn("dbo.Payments", "StatusId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "StatusId", c => c.Int(nullable: false));
            DropColumn("dbo.Payments", "Status");
            CreateIndex("dbo.Payments", "StatusId");
            AddForeignKey("dbo.Payments", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
        }
    }
}
