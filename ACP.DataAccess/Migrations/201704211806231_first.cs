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
                "dbo.BankAccounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AbaRouting = c.String(),
                        BankAccountNumber = c.String(),
                        Type = c.Int(nullable: false),
                        BankName = c.String(),
                        AccountName = c.String(),
                        Lock = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        CreditCardId = c.Int(),
                        CurrencyId = c.Int(nullable: false),
                        BankAccountId = c.Int(),
                        Status = c.Int(nullable: false),
                        PaymentMethod = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .ForeignKey("dbo.CreditCards", t => t.CreditCardId)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .Index(t => t.BookingId)
                .Index(t => t.CreditCardId)
                .Index(t => t.CurrencyId)
                .Index(t => t.BankAccountId);
            
            CreateTable(
                "dbo.Bookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UserId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CarId = c.Int(nullable: false),
                        TravelDetailsId = c.Int(nullable: false),
                        SourceCode = c.String(),
                        AgentReference = c.String(),
                        BookingReference = c.String(),
                        Cost = c.Double(nullable: false),
                        Message = c.String(),
                        Status = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.TravelDetails", t => t.TravelDetailsId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.CustomerId)
                .Index(t => t.CarId)
                .Index(t => t.TravelDetailsId);
            
            CreateTable(
                "dbo.BookingLinks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingId = c.Int(nullable: false),
                        SlotId = c.Int(nullable: false),
                        AvailabilityId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Availabilities", t => t.AvailabilityId)
                .ForeignKey("dbo.Bookings", t => t.BookingId)
                .ForeignKey("dbo.Slots", t => t.SlotId)
                .Index(t => t.BookingId)
                .Index(t => t.SlotId)
                .Index(t => t.AvailabilityId);
            
            CreateTable(
                "dbo.Availabilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        SlotId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Slots", t => t.SlotId, cascadeDelete: true)
                .Index(t => t.SlotId);
            
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
                "dbo.BookingEntities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
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
                .ForeignKey("dbo.RootBookingEntities", t => t.RootBookingEntityId, cascadeDelete: true)
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
                        BookingId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bookings", t => t.BookingId, cascadeDelete: true)
                .ForeignKey("dbo.BookingEntities", t => t.BookingEntityId, cascadeDelete: true)
                .Index(t => t.BookingEntityId)
                .Index(t => t.BookingId);
            
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
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Rating = c.Double(nullable: false),
                        Comments = c.String(),
                        Subject = c.String(),
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
                        Code = c.String(),
                        Website = c.String(),
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
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Initials = c.String(),
                        Forename = c.String(),
                        Surname = c.String(),
                        AddressId = c.Int(nullable: false),
                        Telephone = c.String(),
                        Mobile = c.String(),
                        Fax = c.String(),
                        Email = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
            CreateTable(
                "dbo.TravelDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OutboundTerminal = c.String(),
                        ReturnboundTerminal = c.String(),
                        OutboundFlight = c.String(),
                        ReturnFlight = c.String(),
                        ReturnDate = c.DateTime(nullable: false),
                        OutboundDate = c.DateTime(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Gender = c.Int(nullable: false),
                        DOB = c.DateTime(nullable: false),
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
                "dbo.CreditCards",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.String(),
                        PlainNumber = c.String(),
                        ExpiryDate = c.DateTime(nullable: false),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        GateWayKey = c.String(),
                        Lock = c.Boolean(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Currencies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Symbol = c.String(),
                        Code = c.String(),
                        CountryCode = c.String(),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Payments", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Payments", "CreditCardId", "dbo.CreditCards");
            DropForeignKey("dbo.Payments", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Bookings", "TravelDetailsId", "dbo.TravelDetails");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Bookings", "CarId", "dbo.Cars");
            DropForeignKey("dbo.BookingLinks", "SlotId", "dbo.Slots");
            DropForeignKey("dbo.BookingLinks", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.BookingLinks", "AvailabilityId", "dbo.Availabilities");
            DropForeignKey("dbo.Slots", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.BookingServices", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.RootBookingEntities", "StatusId", "dbo.Status");
            DropForeignKey("dbo.RootBookingProperties", "RootBookingEntityId", "dbo.RootBookingEntities");
            DropForeignKey("dbo.BookingEntities", "RootBookingEntityId", "dbo.RootBookingEntities");
            DropForeignKey("dbo.RootBookingEntities", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Reviews", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.Properties", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.HourPrices", "DayPriceId", "dbo.DayPrices");
            DropForeignKey("dbo.DayPrices", "BookingPricingId", "dbo.BookingPricings");
            DropForeignKey("dbo.BookingPricings", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.Extras", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.Extras", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.BookingEntities", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Availabilities", "SlotId", "dbo.Slots");
            DropForeignKey("dbo.Payments", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Users", new[] { "AddressId" });
            DropIndex("dbo.Customers", new[] { "AddressId" });
            DropIndex("dbo.BookingServices", new[] { "BookingEntityId" });
            DropIndex("dbo.RootBookingProperties", new[] { "RootBookingEntityId" });
            DropIndex("dbo.RootBookingEntities", new[] { "StatusId" });
            DropIndex("dbo.RootBookingEntities", new[] { "AddressId" });
            DropIndex("dbo.Reviews", new[] { "BookingEntityId" });
            DropIndex("dbo.Properties", new[] { "BookingEntityId" });
            DropIndex("dbo.HourPrices", new[] { "DayPriceId" });
            DropIndex("dbo.DayPrices", new[] { "BookingPricingId" });
            DropIndex("dbo.BookingPricings", new[] { "BookingEntityId" });
            DropIndex("dbo.Extras", new[] { "BookingId" });
            DropIndex("dbo.Extras", new[] { "BookingEntityId" });
            DropIndex("dbo.BookingEntities", new[] { "RootBookingEntityId" });
            DropIndex("dbo.BookingEntities", new[] { "AddressId" });
            DropIndex("dbo.Slots", new[] { "BookingEntityId" });
            DropIndex("dbo.Availabilities", new[] { "SlotId" });
            DropIndex("dbo.BookingLinks", new[] { "AvailabilityId" });
            DropIndex("dbo.BookingLinks", new[] { "SlotId" });
            DropIndex("dbo.BookingLinks", new[] { "BookingId" });
            DropIndex("dbo.Bookings", new[] { "TravelDetailsId" });
            DropIndex("dbo.Bookings", new[] { "CarId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Payments", new[] { "BankAccountId" });
            DropIndex("dbo.Payments", new[] { "CurrencyId" });
            DropIndex("dbo.Payments", new[] { "CreditCardId" });
            DropIndex("dbo.Payments", new[] { "BookingId" });
            DropTable("dbo.Currencies");
            DropTable("dbo.CreditCards");
            DropTable("dbo.Users");
            DropTable("dbo.TravelDetails");
            DropTable("dbo.Customers");
            DropTable("dbo.Cars");
            DropTable("dbo.BookingServices");
            DropTable("dbo.Status");
            DropTable("dbo.RootBookingProperties");
            DropTable("dbo.RootBookingEntities");
            DropTable("dbo.Reviews");
            DropTable("dbo.Properties");
            DropTable("dbo.HourPrices");
            DropTable("dbo.DayPrices");
            DropTable("dbo.BookingPricings");
            DropTable("dbo.Extras");
            DropTable("dbo.BookingEntities");
            DropTable("dbo.Slots");
            DropTable("dbo.Availabilities");
            DropTable("dbo.BookingLinks");
            DropTable("dbo.Bookings");
            DropTable("dbo.Payments");
            DropTable("dbo.BankAccounts");
            DropTable("dbo.Addresses");
        }
    }
}
