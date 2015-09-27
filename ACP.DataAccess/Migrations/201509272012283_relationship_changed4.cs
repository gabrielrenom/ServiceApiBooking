namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationship_changed4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Payments", new[] { "CreditCardId" });
            AlterColumn("dbo.Payments", "CreditCardId", c => c.Int());
            CreateIndex("dbo.Payments", "CreditCardId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Payments", new[] { "CreditCardId" });
            AlterColumn("dbo.Payments", "CreditCardId", c => c.Int(nullable: false));
            CreateIndex("dbo.Payments", "CreditCardId");
        }
    }
}
