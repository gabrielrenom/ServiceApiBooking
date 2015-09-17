namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statuschanged : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Status", "StatusType", c => c.Int(nullable: false));
            DropColumn("dbo.Status", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Status", "Name", c => c.String());
            DropColumn("dbo.Status", "StatusType");
        }
    }
}
