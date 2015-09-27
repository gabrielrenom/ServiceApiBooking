namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users_and_cars_removed_from_bookings_from_now : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "CarId" });
            DropColumn("dbo.Bookings", "UserId");
            DropColumn("dbo.Bookings", "CarId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "CarId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "CarId");
            CreateIndex("dbo.Bookings", "UserId");
            AddForeignKey("dbo.Bookings", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Bookings", "CarId", "dbo.Cars", "Id");
        }
    }
}
