namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
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
                        AddressId = c.Int(nullable: false),
                        RootBookingEntityId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.RootBookingEntities", t => t.RootBookingEntityId)
                .Index(t => t.AddressId)
                .Index(t => t.RootBookingEntityId);
            
            CreateTable(
                "dbo.BookingPricings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        BookingEntityId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingEntities", t => t.BookingEntityId, cascadeDelete: true)
                .Index(t => t.BookingEntityId);
            
            CreateTable(
                "dbo.DayPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        Dayprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        BookingPricingId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingPricings", t => t.BookingPricingId, cascadeDelete: true)
                .Index(t => t.BookingPricingId);
            
            CreateTable(
                "dbo.HourPrices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HourMinute = c.DateTime(nullable: false),
                        Hourprice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DayPriceId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayPrices", t => t.DayPriceId, cascadeDelete: true)
                .Index(t => t.DayPriceId);
            
            CreateTable(
                "dbo.RootBookingEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Telephone = c.String(),
                        AddressId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.AddressId)
                .Index(t => t.StatusId);
            
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
            DropForeignKey("dbo.BookingEntities", "RootBookingEntityId", "dbo.RootBookingEntities");
            DropForeignKey("dbo.RootBookingEntities", "StatusId", "dbo.Status");
            DropForeignKey("dbo.RootBookingEntities", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.HourPrices", "DayPriceId", "dbo.DayPrices");
            DropForeignKey("dbo.DayPrices", "BookingPricingId", "dbo.BookingPricings");
            DropForeignKey("dbo.BookingPricings", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.BookingEntities", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "Address_Id" });
            DropIndex("dbo.Bookings", new[] { "User_Id" });
            DropIndex("dbo.Bookings", new[] { "Status_Id" });
            DropIndex("dbo.Bookings", new[] { "Entity_Id" });
            DropIndex("dbo.BookingServices", new[] { "BookingEntity_Id" });
            DropIndex("dbo.RootBookingEntities", new[] { "StatusId" });
            DropIndex("dbo.RootBookingEntities", new[] { "AddressId" });
            DropIndex("dbo.HourPrices", new[] { "DayPriceId" });
            DropIndex("dbo.DayPrices", new[] { "BookingPricingId" });
            DropIndex("dbo.BookingPricings", new[] { "BookingEntityId" });
            DropIndex("dbo.BookingEntities", new[] { "RootBookingEntityId" });
            DropIndex("dbo.BookingEntities", new[] { "AddressId" });
            DropTable("dbo.Users");
            DropTable("dbo.Bookings");
            DropTable("dbo.BookingServices");
            DropTable("dbo.Status");
            DropTable("dbo.RootBookingEntities");
            DropTable("dbo.HourPrices");
            DropTable("dbo.DayPrices");
            DropTable("dbo.BookingPricings");
            DropTable("dbo.BookingEntities");
            DropTable("dbo.Addresses");
        }
    }
}
