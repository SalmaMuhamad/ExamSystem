namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblsAddedColHall : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserCourses", "HallID", c => c.Int());
            AddColumn("dbo.Courses", "StartedAt", c => c.DateTime());
            AddColumn("dbo.Courses", "CompletedAt", c => c.DateTime());
            CreateIndex("dbo.UserCourses", "HallID");
            AddForeignKey("dbo.UserCourses", "HallID", "dbo.Halls", "ID");
            DropColumn("dbo.UserCourses", "StartedAt");
            DropColumn("dbo.UserCourses", "CompletedAt");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserCourses", "CompletedAt", c => c.DateTime());
            AddColumn("dbo.UserCourses", "StartedAt", c => c.DateTime());
            DropForeignKey("dbo.UserCourses", "HallID", "dbo.Halls");
            DropIndex("dbo.UserCourses", new[] { "HallID" });
            DropColumn("dbo.Courses", "CompletedAt");
            DropColumn("dbo.Courses", "StartedAt");
            DropColumn("dbo.UserCourses", "HallID");
        }
    }
}
