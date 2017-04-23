namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sec : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Slots", "BookingEntityId", "dbo.BookingEntities");
            AddForeignKey("dbo.Slots", "BookingEntityId", "dbo.BookingEntities", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Slots", "BookingEntityId", "dbo.BookingEntities");
            AddForeignKey("dbo.Slots", "BookingEntityId", "dbo.BookingEntities", "Id");
        }
    }
}
