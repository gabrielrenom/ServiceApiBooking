namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationship_changed3 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Payments", new[] { "BankAccountId" });
            AlterColumn("dbo.Payments", "BankAccountId", c => c.Int());
            CreateIndex("dbo.Payments", "BankAccountId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Payments", new[] { "BankAccountId" });
            AlterColumn("dbo.Payments", "BankAccountId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "BankAccountId");
        }
    }
}
