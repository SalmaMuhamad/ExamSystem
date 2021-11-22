namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblsAddedColHall_UC : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserCourses", "CategoryID", "dbo.Categories");
            DropIndex("dbo.UserCourses", new[] { "CategoryID" });
            DropColumn("dbo.UserCourses", "CategoryID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserCourses", "CategoryID", c => c.Int());
            CreateIndex("dbo.UserCourses", "CategoryID");
            AddForeignKey("dbo.UserCourses", "CategoryID", "dbo.Categories", "ID");
        }
    }
}
