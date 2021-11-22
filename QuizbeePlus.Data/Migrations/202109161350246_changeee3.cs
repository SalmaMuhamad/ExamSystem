namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeee3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "MinistryId", "dbo.Ministries");
            DropIndex("dbo.Users", new[] { "MinistryId" });
            RenameColumn(table: "dbo.Users", name: "MinistryId", newName: "Ministry_ID");
            AlterColumn("dbo.Users", "Ministry_ID", c => c.Int());
            CreateIndex("dbo.Users", "Ministry_ID");
            AddForeignKey("dbo.Users", "Ministry_ID", "dbo.Ministries", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "Ministry_ID", "dbo.Ministries");
            DropIndex("dbo.Users", new[] { "Ministry_ID" });
            AlterColumn("dbo.Users", "Ministry_ID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Users", name: "Ministry_ID", newName: "MinistryId");
            CreateIndex("dbo.Users", "MinistryId");
            AddForeignKey("dbo.Users", "MinistryId", "dbo.Ministries", "ID", cascadeDelete: true);
        }
    }
}
