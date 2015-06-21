namespace ACP.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addressaddedinUser : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Users", new[] { "Address_Id" });
            DropColumn("dbo.Users", "AddressId");
            RenameColumn(table: "dbo.Users", name: "Address_Id", newName: "AddressId");
            AlterColumn("dbo.Users", "AddressId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Users", "AddressId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "AddressId" });
            AlterColumn("dbo.Users", "AddressId", c => c.Int());
            AlterColumn("dbo.Users", "AddressId", c => c.String());
            RenameColumn(table: "dbo.Users", name: "AddressId", newName: "Address_Id");
            AddColumn("dbo.Users", "AddressId", c => c.String());
            CreateIndex("dbo.Users", "Address_Id");
        }
    }
}
