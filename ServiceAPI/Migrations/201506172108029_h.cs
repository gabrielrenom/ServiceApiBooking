namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class h : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.HourPrices", "HourMinute", c => c.DateTime(nullable: false));
            AddColumn("dbo.HourPrices", "Hourprice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.HourPrices", "Hourprice");
            DropColumn("dbo.HourPrices", "HourMinute");
        }
    }
}
