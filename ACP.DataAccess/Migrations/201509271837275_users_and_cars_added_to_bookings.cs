namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users_and_cars_added_to_bookings : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bookings", "UserId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "CarId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "UserId");
            CreateIndex("dbo.Bookings", "CarId");
            AddForeignKey("dbo.Bookings", "CarId", "dbo.Cars", "Id");
            AddForeignKey("dbo.Bookings", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "CarId", "dbo.Cars");
            DropIndex("dbo.Bookings", new[] { "CarId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropColumn("dbo.Bookings", "CarId");
            DropColumn("dbo.Bookings", "UserId");
        }
    }
}
