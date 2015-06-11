namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class firstmigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Address1 = c.String(),
                        Address2 = c.String(),
                        Postcode = c.String(),
                        County = c.String(),
                        Country = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BookingEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Image = c.Binary(),
                        Logo = c.Binary(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comission = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Sameday = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Address_Id = c.Int(),
                        Entity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.RootBookingEntities", t => t.Entity_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.Entity_Id);
            
            CreateTable(
                "dbo.RootBookingEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Telephone = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Address_Id = c.Int(),
                        Status_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .ForeignKey("dbo.Status", t => t.Status_Id)
                .Index(t => t.Address_Id)
                .Index(t => t.Status_Id);
            
            CreateTable(
                "dbo.Status",
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
            
            CreateTable(
                "dbo.BookingPricings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        BookingEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingEntities", t => t.BookingEntity_Id)
                .Index(t => t.BookingEntity_Id);
            
            CreateTable(
                "dbo.DayPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        Dayprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        BookingPricing_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingPricings", t => t.BookingPricing_Id)
                .Index(t => t.BookingPricing_Id);
            
            CreateTable(
                "dbo.HourPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        DayPrice_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayPrices", t => t.DayPrice_Id)
                .Index(t => t.DayPrice_Id);
            
            CreateTable(
                "dbo.BookingServices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        BookingEntity_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingEntities", t => t.BookingEntity_Id)
                .Index(t => t.BookingEntity_Id);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Entity_Id = c.Int(),
                        Status_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingEntities", t => t.Entity_Id)
                .ForeignKey("dbo.Status", t => t.Status_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Entity_Id)
                .Index(t => t.Status_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Bookings", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.Bookings", "Entity_Id", "dbo.BookingEntities");
            DropForeignKey("dbo.BookingServices", "BookingEntity_Id", "dbo.BookingEntities");
            DropForeignKey("dbo.HourPrices", "DayPrice_Id", "dbo.DayPrices");
            DropForeignKey("dbo.DayPrices", "BookingPricing_Id", "dbo.BookingPricings");
            DropForeignKey("dbo.BookingPricings", "BookingEntity_Id", "dbo.BookingEntities");
            DropForeignKey("dbo.RootBookingEntities", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.BookingEntities", "Entity_Id", "dbo.RootBookingEntities");
            DropForeignKey("dbo.RootBookingEntities", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.BookingEntities", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "Address_Id" });
            DropIndex("dbo.Bookings", new[] { "User_Id" });
            DropIndex("dbo.Bookings", new[] { "Status_Id" });
            DropIndex("dbo.Bookings", new[] { "Entity_Id" });
            DropIndex("dbo.BookingServices", new[] { "BookingEntity_Id" });
            DropIndex("dbo.HourPrices", new[] { "DayPrice_Id" });
            DropIndex("dbo.DayPrices", new[] { "BookingPricing_Id" });
            DropIndex("dbo.BookingPricings", new[] { "BookingEntity_Id" });
            DropIndex("dbo.RootBookingEntities", new[] { "Status_Id" });
            DropIndex("dbo.RootBookingEntities", new[] { "Address_Id" });
            DropIndex("dbo.BookingEntities", new[] { "Entity_Id" });
            DropIndex("dbo.BookingEntities", new[] { "Address_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Bookings");
            DropTable("dbo.BookingServices");
            DropTable("dbo.HourPrices");
            DropTable("dbo.DayPrices");
            DropTable("dbo.BookingPricings");
            DropTable("dbo.Status");
            DropTable("dbo.RootBookingEntities");
            DropTable("dbo.BookingEntities");
            DropTable("dbo.Addresses");
        }
    }
}
