namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class properties1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Properties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingEntityId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingEntities", t => t.BookingEntityId, cascadeDelete: true)
                .Index(t => t.BookingEntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Properties", "BookingEntityId", "dbo.BookingEntities");
            DropIndex("dbo.Properties", new[] { "BookingEntityId" });
            DropTable("dbo.Properties");
        }
    }
}
