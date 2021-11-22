namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        StartedAt = c.DateTime(),
                        CompletedAt = c.DateTime(),
                        StudentID = c.String(maxLength: 128),
                        ModifiedOn = c.DateTime(),
                        Category_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.StudentID)
                .ForeignKey("dbo.Categories", t => t.Category_ID)
                .Index(t => t.StudentID)
                .Index(t => t.Category_ID);
            
            CreateTable(
                "dbo.Exam_Question",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ExamID = c.Int(),
                        QuestionID = c.Int(nullable: false),
                        AnsweredAt = c.DateTime(),
                        IsCorrect = c.Boolean(nullable: false),
                        OptionID = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exams", t => t.ExamID)
                .ForeignKey("dbo.Options", t => t.OptionID, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.ExamID)
                .Index(t => t.QuestionID)
                .Index(t => t.OptionID);
            
            CreateTable(
                "dbo.Options",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Answer = c.String(),
                        IsCorrect = c.Boolean(nullable: false),
                        ModifiedOn = c.DateTime(),
                        Question_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Questions", t => t.Question_ID)
                .Index(t => t.Question_ID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Desciption = c.String(),
                        ModifiedOn = c.DateTime(),
                        Category_ID = c.Int(),
                        Degree_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_ID)
                .ForeignKey("dbo.Degrees", t => t.Degree_ID)
                .Index(t => t.Category_ID)
                .Index(t => t.Degree_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Photo = c.String(),
                        RegisteredOn = c.DateTime(),
                        MinistryId = c.Int(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Ministries", t => t.MinistryId, cascadeDelete: true)
                .Index(t => t.MinistryId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Ministries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Degrees",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Questions", "Degree_ID", "dbo.Degrees");
            DropForeignKey("dbo.Exams", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "MinistryId", "dbo.Ministries");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.Exams", "StudentID", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Options", "Question_ID", "dbo.Questions");
            DropForeignKey("dbo.Exam_Question", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Questions", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Exam_Question", "OptionID", "dbo.Options");
            DropForeignKey("dbo.Exam_Question", "ExamID", "dbo.Exams");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Users", new[] { "MinistryId" });
            DropIndex("dbo.Questions", new[] { "Degree_ID" });
            DropIndex("dbo.Questions", new[] { "Category_ID" });
            DropIndex("dbo.Options", new[] { "Question_ID" });
            DropIndex("dbo.Exam_Question", new[] { "OptionID" });
            DropIndex("dbo.Exam_Question", new[] { "QuestionID" });
            DropIndex("dbo.Exam_Question", new[] { "ExamID" });
            DropIndex("dbo.Exams", new[] { "Category_ID" });
            DropIndex("dbo.Exams", new[] { "StudentID" });
            DropTable("dbo.Roles");
            DropTable("dbo.Images");
            DropTable("dbo.Degrees");
            DropTable("dbo.UserRoles");
            DropTable("dbo.Ministries");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Questions");
            DropTable("dbo.Options");
            DropTable("dbo.Exam_Question");
            DropTable("dbo.Exams");
            DropTable("dbo.Categories");
        }
    }
}
