namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extrasandpriceupdated : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Extras", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Extras", "Price", c => c.String());
        }
    }
}
