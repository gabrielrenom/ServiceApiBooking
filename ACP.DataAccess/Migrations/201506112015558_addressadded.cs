namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addressadded : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookingEntities", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.BookingEntities", new[] { "Address_Id" });
            RenameColumn(table: "dbo.BookingEntities", name: "Address_Id", newName: "AddressId");
            AlterColumn("dbo.BookingEntities", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.BookingEntities", "AddressId");
            AddForeignKey("dbo.BookingEntities", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingEntities", "AddressId", "dbo.Addresses");
            DropIndex("dbo.BookingEntities", new[] { "AddressId" });
            AlterColumn("dbo.BookingEntities", "AddressId", c => c.Int());
            RenameColumn(table: "dbo.BookingEntities", name: "AddressId", newName: "Address_Id");
            CreateIndex("dbo.BookingEntities", "Address_Id");
            AddForeignKey("dbo.BookingEntities", "Address_Id", "dbo.Addresses", "Id");
        }
    }
}
