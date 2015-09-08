namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newone : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RootBookingProperties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(),
                        Value = c.String(),
                        Type = c.Int(nullable: false),
                        RootBookingEntityId = c.Int(nullable: false),
                        CreatedBy = c.String(),
                        ModifiedBy = c.String(),
                        Created = c.DateTime(),
                        Modified = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RootBookingEntities", t => t.RootBookingEntityId, cascadeDelete: true)
                .Index(t => t.RootBookingEntityId);
            
            AddColumn("dbo.Addresses", "City", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RootBookingProperties", "RootBookingEntityId", "dbo.RootBookingEntities");
            DropIndex("dbo.RootBookingProperties", new[] { "RootBookingEntityId" });
            DropColumn("dbo.Addresses", "City");
            DropTable("dbo.RootBookingProperties");
        }
    }
}
