namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newsservicetable : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Services", newName: "BookingServices");
            AlterColumn("dbo.BookingServices", "Name", c => c.String(nullable: false, maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.BookingServices", "Name", c => c.String());
            RenameTable(name: "dbo.BookingServices", newName: "Services");
        }
    }
}
