namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customer_removed_with_payments : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Payments", new[] { "CustomerId" });
            DropColumn("dbo.Payments", "CustomerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "CustomerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "CustomerId");
            AddForeignKey("dbo.Payments", "CustomerId", "dbo.Customers", "Id");
        }
    }
}
