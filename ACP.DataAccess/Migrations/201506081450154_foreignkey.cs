namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignkey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BookingPricings", "BookingEntity_Id", "dbo.BookingEntities");
            DropIndex("dbo.BookingPricings", new[] { "BookingEntity_Id" });
            RenameColumn(table: "dbo.BookingPricings", name: "BookingEntity_Id", newName: "BookingEntityId");
            AlterColumn("dbo.BookingPricings", "BookingEntityId", c => c.Int(nullable: false));
            CreateIndex("dbo.BookingPricings", "BookingEntityId");
            AddForeignKey("dbo.BookingPricings", "BookingEntityId", "dbo.BookingEntities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookingPricings", "BookingEntityId", "dbo.BookingEntities");
            DropIndex("dbo.BookingPricings", new[] { "BookingEntityId" });
            AlterColumn("dbo.BookingPricings", "BookingEntityId", c => c.Int());
            RenameColumn(table: "dbo.BookingPricings", name: "BookingEntityId", newName: "BookingEntity_Id");
            CreateIndex("dbo.BookingPricings", "BookingEntity_Id");
            AddForeignKey("dbo.BookingPricings", "BookingEntity_Id", "dbo.BookingEntities", "Id");
        }
    }
}
