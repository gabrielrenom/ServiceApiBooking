namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_zone : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Availabilities", "BookingEntityId", "dbo.BookingEntities");
            DropIndex("dbo.Availabilities", new[] { "BookingEntityId" });
            CreateTable(
                "dbo.Zones",
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
                .ForeignKey("dbo.BookingEntities", t => t.BookingEntityId)
                .Index(t => t.BookingEntityId);
            
            AddColumn("dbo.Availabilities", "ZoneId", c => c.Int(nullable: false));
            CreateIndex("dbo.Availabilities", "ZoneId");
            AddForeignKey("dbo.Availabilities", "ZoneId", "dbo.Zones", "Id", cascadeDelete: true);
            DropColumn("dbo.Availabilities", "BookingEntityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Availabilities", "BookingEntityId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Zones", "BookingEntityId", "dbo.BookingEntities");
            DropForeignKey("dbo.Availabilities", "ZoneId", "dbo.Zones");
            DropIndex("dbo.Availabilities", new[] { "ZoneId" });
            DropIndex("dbo.Zones", new[] { "BookingEntityId" });
            DropColumn("dbo.Availabilities", "ZoneId");
            DropTable("dbo.Zones");
            CreateIndex("dbo.Availabilities", "BookingEntityId");
            AddForeignKey("dbo.Availabilities", "BookingEntityId", "dbo.BookingEntities", "Id", cascadeDelete: true);
        }
    }
}
