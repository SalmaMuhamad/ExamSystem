namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StartedAt = c.DateTime(),
                        CompletedAt = c.DateTime(),
                        CourseID = c.Int(),
                        CategoryID = c.Int(),
                        UserID = c.String(),
                        ModifiedOn = c.DateTime(),
                        QuizbeeUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.CourseID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .ForeignKey("dbo.Users", t => t.QuizbeeUser_Id)
                .Index(t => t.CourseID)
                .Index(t => t.CategoryID)
                .Index(t => t.QuizbeeUser_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CategoryID = c.Int(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.CategoryID)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.Halls",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        HallNumber = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCourses", "QuizbeeUser_Id", "dbo.Users");
            DropForeignKey("dbo.UserCourses", "CategoryID", "dbo.Categories");
            DropForeignKey("dbo.UserCourses", "CourseID", "dbo.Courses");
            DropForeignKey("dbo.Courses", "CategoryID", "dbo.Categories");
            DropIndex("dbo.Courses", new[] { "CategoryID" });
            DropIndex("dbo.UserCourses", new[] { "QuizbeeUser_Id" });
            DropIndex("dbo.UserCourses", new[] { "CategoryID" });
            DropIndex("dbo.UserCourses", new[] { "CourseID" });
            DropTable("dbo.Halls");
            DropTable("dbo.Courses");
            DropTable("dbo.UserCourses");
        }
    }
}
