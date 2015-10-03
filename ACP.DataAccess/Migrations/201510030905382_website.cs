namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class website : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RootBookingEntities", "Code", c => c.String());
            AddColumn("dbo.RootBookingEntities", "Website", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.RootBookingEntities", "Website");
            DropColumn("dbo.RootBookingEntities", "Code");
        }
    }
}
