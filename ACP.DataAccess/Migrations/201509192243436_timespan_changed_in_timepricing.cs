namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timespan_changed_in_timepricing : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HourPrices", "HourMinute", c => c.Time(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HourPrices", "HourMinute", c => c.DateTime(nullable: false));
        }
    }
}
