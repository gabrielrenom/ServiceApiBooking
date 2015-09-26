namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookings_payments_modified : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Payments", "StatusId", "dbo.Status");
            DropIndex("dbo.Bookings", new[] { "PaymentId" });
            CreateIndex("dbo.Payments", "BookingId");
            AddForeignKey("dbo.Payments", "BookingId", "dbo.Bookings", "Id");
            AddForeignKey("dbo.Payments", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
            DropColumn("dbo.Bookings", "PaymentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "PaymentId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Payments", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Payments", "BookingId", "dbo.Bookings");
            DropIndex("dbo.Payments", new[] { "BookingId" });
            CreateIndex("dbo.Bookings", "PaymentId");
            AddForeignKey("dbo.Payments", "StatusId", "dbo.Status", "Id");
            AddForeignKey("dbo.Bookings", "PaymentId", "dbo.Payments", "Id");
        }
    }
}
