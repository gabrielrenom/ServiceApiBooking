namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bookingsadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.BookingEntities", "RootBookingEntityId", "dbo.RootBookingEntities");
            DropForeignKey("dbo.Bookings", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropIndex("dbo.Bookings", new[] { "BookingEntityId" });
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
                        CustomerId = c.Int(nullable: false),
                        PaymentMethodId = c.Int(nullable: false),
                        CreditCardId = c.Int(nullable: false),
                        CurrencyId = c.Int(nullable: false),
                        StatusId = c.Int(nullable: false),
                        BankAccountId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BankAccounts", t => t.BankAccountId)
                .ForeignKey("dbo.CreditCards", t => t.CreditCardId)
                .ForeignKey("dbo.Currencies", t => t.CurrencyId)
                .ForeignKey("dbo.Customers", t => t.CustomerId)
                .ForeignKey("dbo.PaymentMethods", t => t.PaymentMethodId)
                .ForeignKey("dbo.Status", t => t.StatusId)
                .Index(t => t.CustomerId)
                .Index(t => t.PaymentMethodId)
                .Index(t => t.CreditCardId)
                .Index(t => t.CurrencyId)
                .Index(t => t.StatusId)
                .Index(t => t.BankAccountId);
            
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
                        Lock = c.String(),
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
            
            AddColumn("dbo.Extras", "BookingId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "CustomerId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "PaymentId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "CarId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "TravelDetailsId", c => c.Int(nullable: false));
            AddColumn("dbo.Bookings", "SourceCode", c => c.String());
            AddColumn("dbo.Bookings", "AgentReference", c => c.String());
            AddColumn("dbo.Bookings", "BookingReference", c => c.String());
            AddColumn("dbo.Bookings", "Cost", c => c.Double(nullable: false));
            AddColumn("dbo.Bookings", "Message", c => c.String());
            CreateIndex("dbo.Bookings", "CustomerId");
            CreateIndex("dbo.Bookings", "PaymentId");
            CreateIndex("dbo.Bookings", "CarId");
            CreateIndex("dbo.Bookings", "TravelDetailsId");
            CreateIndex("dbo.Extras", "BookingId");
            AddForeignKey("dbo.Extras", "BookingId", "dbo.Bookings", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "CarId", "dbo.Cars", "Id");
            AddForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Bookings", "PaymentId", "dbo.Payments", "Id");
            AddForeignKey("dbo.Bookings", "TravelDetailsId", "dbo.TravelDetails", "Id");
            AddForeignKey("dbo.BookingEntities", "RootBookingEntityId", "dbo.RootBookingEntities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "StatusId", "dbo.Status", "Id");
            AddForeignKey("dbo.Bookings", "UserId", "dbo.Users", "Id");
            DropColumn("dbo.Bookings", "BookingEntityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings", "BookingEntityId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "StatusId", "dbo.Status");
            DropForeignKey("dbo.BookingEntities", "RootBookingEntityId", "dbo.RootBookingEntities");
            DropForeignKey("dbo.Payments", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Payments", "PaymentMethodId", "dbo.PaymentMethods");
            DropForeignKey("dbo.Payments", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Payments", "CurrencyId", "dbo.Currencies");
            DropForeignKey("dbo.Payments", "CreditCardId", "dbo.CreditCards");
            DropForeignKey("dbo.Bookings", "TravelDetailsId", "dbo.TravelDetails");
            DropForeignKey("dbo.Bookings", "PaymentId", "dbo.Payments");
            DropForeignKey("dbo.Bookings", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.Customers", "AddressId", "dbo.Addresses");
            DropForeignKey("dbo.Bookings", "CarId", "dbo.Cars");
            DropForeignKey("dbo.BookingLinks", "SlotId", "dbo.Slots");
            DropForeignKey("dbo.BookingLinks", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.BookingLinks", "AvailabilityId", "dbo.Availabilities");
            DropForeignKey("dbo.Extras", "BookingId", "dbo.Bookings");
            DropForeignKey("dbo.Payments", "BankAccountId", "dbo.BankAccounts");
            DropIndex("dbo.Customers", new[] { "AddressId" });
            DropIndex("dbo.Extras", new[] { "BookingId" });
            DropIndex("dbo.BookingLinks", new[] { "AvailabilityId" });
            DropIndex("dbo.BookingLinks", new[] { "SlotId" });
            DropIndex("dbo.BookingLinks", new[] { "BookingId" });
            DropIndex("dbo.Bookings", new[] { "TravelDetailsId" });
            DropIndex("dbo.Bookings", new[] { "CarId" });
            DropIndex("dbo.Bookings", new[] { "PaymentId" });
            DropIndex("dbo.Bookings", new[] { "CustomerId" });
            DropIndex("dbo.Payments", new[] { "BankAccountId" });
            DropIndex("dbo.Payments", new[] { "StatusId" });
            DropIndex("dbo.Payments", new[] { "CurrencyId" });
            DropIndex("dbo.Payments", new[] { "CreditCardId" });
            DropIndex("dbo.Payments", new[] { "PaymentMethodId" });
            DropIndex("dbo.Payments", new[] { "CustomerId" });
            DropColumn("dbo.Bookings", "Message");
            DropColumn("dbo.Bookings", "Cost");
            DropColumn("dbo.Bookings", "BookingReference");
            DropColumn("dbo.Bookings", "AgentReference");
            DropColumn("dbo.Bookings", "SourceCode");
            DropColumn("dbo.Bookings", "TravelDetailsId");
            DropColumn("dbo.Bookings", "CarId");
            DropColumn("dbo.Bookings", "PaymentId");
            DropColumn("dbo.Bookings", "CustomerId");
            DropColumn("dbo.Extras", "BookingId");
            DropTable("dbo.PaymentMethods");
            DropTable("dbo.Currencies");
            DropTable("dbo.CreditCards");
            DropTable("dbo.TravelDetails");
            DropTable("dbo.Customers");
            DropTable("dbo.BookingLinks");
            DropTable("dbo.Payments");
            DropTable("dbo.BankAccounts");
            CreateIndex("dbo.Bookings", "BookingEntityId");
            AddForeignKey("dbo.Bookings", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BookingEntities", "RootBookingEntityId", "dbo.RootBookingEntities", "Id");
            AddForeignKey("dbo.Bookings", "BookingEntityId", "dbo.BookingEntities", "Id", cascadeDelete: true);
        }
    }
}
