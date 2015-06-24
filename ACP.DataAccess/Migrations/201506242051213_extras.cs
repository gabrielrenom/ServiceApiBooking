namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extras : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Extras",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.String(),
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
            DropForeignKey("dbo.Extras", "BookingEntityId", "dbo.BookingEntities");
            DropIndex("dbo.Extras", new[] { "BookingEntityId" });
            DropTable("dbo.Extras");
        }
    }
}
