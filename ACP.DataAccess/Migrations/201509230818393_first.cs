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
                        City = c.String(),
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
                        EntityType = c.Int(nullable: false),
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
                "dbo.Extras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
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
                        HourMinute = c.Time(nullable: false, precision: 7),
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
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        Type = c.Int(nullable: false),
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
                "dbo.RootBookingProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        Type = c.Int(nullable: false),
                        RootBookingEntityId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RootBookingEntities", t => t.RootBookingEntityId, cascadeDelete: true)
                .Index(t => t.RootBookingEntityId);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StatusType = c.Int(nullable: false),
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
                "dbo.Slots",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        Identifier = c.String(),
                        IsOccupied = c.Boolean(nullable: false),
                        BookingEntityId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingEntities", t => t.BookingEntityId)
                .Index(t => t.BookingEntityId);
            
            CreateTable(
                "dbo.Availabilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        SlotId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Slots", t => t.SlotId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.SlotId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        StatusId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        BookingEntityId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingEntities", t => t.BookingEntityId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.UserId)
                .Index(t => t.BookingEntityId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        AddressId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Registration = c.String(),
                        Make = c.String(),
                        Model = c.String(),
                        Colour = c.String(),
                        UserId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cars", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Bookings", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Bookings", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.Slots", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.Availabilities", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Availabilities", "SlotId", "dbo.Slots");
            DropForeignKey("dbo.BookingServices", "BookingEntity_Id", "dbo.BookingEntities");
            DropForeignKey("dbo.BookingEntities", "RootBookingEntityId", "dbo.RootBookingEntities");
            DropForeignKey("dbo.RootBookingEntities", "StatusId", "dbo.Status");
            DropForeignKey("dbo.RootBookingProperties", "RootBookingEntityId", "dbo.RootBookingEntities");
            DropForeignKey("dbo.RootBookingEntities", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Properties", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.HourPrices", "DayPriceId", "dbo.DayPrices");
            DropForeignKey("dbo.DayPrices", "BookingPricingId", "dbo.BookingPricings");
            DropForeignKey("dbo.BookingPricings", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.Extras", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.BookingEntities", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Cars", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropIndex("dbo.Bookings", new[] { "BookingEntityId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "StatusId" });
            DropIndex("dbo.Availabilities", new[] { "SlotId" });
            DropIndex("dbo.Availabilities", new[] { "StatusId" });
            DropIndex("dbo.Slots", new[] { "BookingEntityId" });
            DropIndex("dbo.BookingServices", new[] { "BookingEntity_Id" });
            DropIndex("dbo.RootBookingProperties", new[] { "RootBookingEntityId" });
            DropIndex("dbo.RootBookingEntities", new[] { "StatusId" });
            DropIndex("dbo.RootBookingEntities", new[] { "AddressId" });
            DropIndex("dbo.Properties", new[] { "BookingEntityId" });
            DropIndex("dbo.HourPrices", new[] { "DayPriceId" });
            DropIndex("dbo.DayPrices", new[] { "BookingPricingId" });
            DropIndex("dbo.BookingPricings", new[] { "BookingEntityId" });
            DropIndex("dbo.Extras", new[] { "BookingEntityId" });
            DropIndex("dbo.BookingEntities", new[] { "RootBookingEntityId" });
            DropIndex("dbo.BookingEntities", new[] { "AddressId" });
            DropTable("dbo.Cars");
            DropTable("dbo.Users");
            DropTable("dbo.Bookings");
            DropTable("dbo.Availabilities");
            DropTable("dbo.Slots");
            DropTable("dbo.BookingServices");
            DropTable("dbo.Status");
            DropTable("dbo.RootBookingProperties");
            DropTable("dbo.RootBookingEntities");
            DropTable("dbo.Properties");
            DropTable("dbo.HourPrices");
            DropTable("dbo.DayPrices");
            DropTable("dbo.BookingPricings");
            DropTable("dbo.Extras");
            DropTable("dbo.BookingEntities");
            DropTable("dbo.Addresses");
        }
    }
}
