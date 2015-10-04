namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addreswhereitwas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RootBookingEntities", "AddressId", "dbo.Addresses");
            AddForeignKey("dbo.RootBookingEntities", "AddressId", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RootBookingEntities", "AddressId", "dbo.Addresses");
            AddForeignKey("dbo.RootBookingEntities", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
    }
}
