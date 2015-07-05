namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingavailability : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Availabilities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        StatusId = c.Int(nullable: false),
                        BookingEntityId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BookingEntities", t => t.BookingEntityId, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.StatusId, cascadeDelete: true)
                .Index(t => t.StatusId)
                .Index(t => t.BookingEntityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Availabilities", "StatusId", "dbo.Status");
            DropForeignKey("dbo.Availabilities", "BookingEntityId", "dbo.BookingEntities");
            DropIndex("dbo.Availabilities", new[] { "BookingEntityId" });
            DropIndex("dbo.Availabilities", new[] { "StatusId" });
            DropTable("dbo.Availabilities");
        }
    }
}
