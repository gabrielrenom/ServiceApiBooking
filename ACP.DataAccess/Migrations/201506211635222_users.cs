namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class users : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bookings", "Entity_Id", "dbo.BookingEntities");
            DropForeignKey("dbo.Bookings", "Status_Id", "dbo.Status");
            DropForeignKey("dbo.Bookings", "User_Id", "dbo.Users");
            DropIndex("dbo.Bookings", new[] { "Entity_Id" });
            DropIndex("dbo.Bookings", new[] { "Status_Id" });
            DropIndex("dbo.Bookings", new[] { "User_Id" });
            RenameColumn(table: "dbo.Bookings", name: "Entity_Id", newName: "BookingEntityId");
            RenameColumn(table: "dbo.Bookings", name: "Status_Id", newName: "StatusId");
            RenameColumn(table: "dbo.Bookings", name: "User_Id", newName: "UserId");
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
            
            AddColumn("dbo.Users", "Email", c => c.String());
            AddColumn("dbo.Users", "Password", c => c.String());
            AddColumn("dbo.Users", "AddressId", c => c.String());
            AlterColumn("dbo.Bookings", "BookingEntityId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "StatusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Bookings", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Bookings", "StatusId");
            CreateIndex("dbo.Bookings", "UserId");
            CreateIndex("dbo.Bookings", "BookingEntityId");
            AddForeignKey("dbo.Bookings", "BookingEntityId", "dbo.BookingEntities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Bookings", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bookings", "UserId", "dbo.Users");
            DropForeignKey("dbo.Bookings", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Bookings", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.Cars", "UserId", "dbo.Users");
            DropIndex("dbo.Cars", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "BookingEntityId" });
            DropIndex("dbo.Bookings", new[] { "UserId" });
            DropIndex("dbo.Bookings", new[] { "StatusId" });
            AlterColumn("dbo.Bookings", "UserId", c => c.Int());
            AlterColumn("dbo.Bookings", "StatusId", c => c.Int());
            AlterColumn("dbo.Bookings", "BookingEntityId", c => c.Int());
            DropColumn("dbo.Users", "AddressId");
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "Email");
            DropTable("dbo.Cars");
            RenameColumn(table: "dbo.Bookings", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Bookings", name: "StatusId", newName: "Status_Id");
            RenameColumn(table: "dbo.Bookings", name: "BookingEntityId", newName: "Entity_Id");
            CreateIndex("dbo.Bookings", "User_Id");
            CreateIndex("dbo.Bookings", "Status_Id");
            CreateIndex("dbo.Bookings", "Entity_Id");
            AddForeignKey("dbo.Bookings", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Bookings", "Status_Id", "dbo.Status", "Id");
            AddForeignKey("dbo.Bookings", "Entity_Id", "dbo.BookingEntities", "Id");
        }
    }
}
