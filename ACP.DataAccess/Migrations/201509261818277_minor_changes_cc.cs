namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minor_changes_cc : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Payments", "PaymentMethodId", "dbo.PaymentMethods");
            DropIndex("dbo.Payments", new[] { "PaymentMethodId" });
            AddColumn("dbo.Payments", "PaymentMethod", c => c.Int(nullable: false));
            AlterColumn("dbo.CreditCards", "Lock", c => c.Boolean(nullable: false));
            DropColumn("dbo.Payments", "PaymentMethodId");
            DropTable("dbo.PaymentMethods");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.PaymentMethods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Payments", "PaymentMethodId", c => c.Int(nullable: false));
            AlterColumn("dbo.CreditCards", "Lock", c => c.String());
            DropColumn("dbo.Payments", "PaymentMethod");
            CreateIndex("dbo.Payments", "PaymentMethodId");
            AddForeignKey("dbo.Payments", "PaymentMethodId", "dbo.PaymentMethods", "Id");
        }
    }
}
