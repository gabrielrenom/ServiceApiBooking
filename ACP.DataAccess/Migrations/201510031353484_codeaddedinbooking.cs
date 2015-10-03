namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codeaddedinbooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingEntities", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookingEntities", "Code");
        }
    }
}
