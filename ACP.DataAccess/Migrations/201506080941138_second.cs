namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.BookingEntities", name: "Entity_Id", newName: "RootBookingEntity_Id");
            RenameIndex(table: "dbo.BookingEntities", name: "IX_Entity_Id", newName: "IX_RootBookingEntity_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.BookingEntities", name: "IX_RootBookingEntity_Id", newName: "IX_Entity_Id");
            RenameColumn(table: "dbo.BookingEntities", name: "RootBookingEntity_Id", newName: "Entity_Id");
        }
    }
}
