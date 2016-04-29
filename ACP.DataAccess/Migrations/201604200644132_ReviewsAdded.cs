namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Author = c.String(),
                        Rating = c.Double(nullable: false),
                        Comments = c.String(),
                        Subject = c.String(),
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
            DropForeignKey("dbo.Reviews", "BookingEntityId", "dbo.BookingEntities");
            DropIndex("dbo.Reviews", new[] { "BookingEntityId" });
            DropTable("dbo.Reviews");
        }
    }
}
