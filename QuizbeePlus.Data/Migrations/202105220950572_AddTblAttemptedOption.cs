namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblAttemptedOption : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AttemptedOptions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Exam_QuestionID = c.Int(nullable: false),
                        OptionID = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Exam_Question", t => t.Exam_QuestionID, cascadeDelete: true)
                .ForeignKey("dbo.Options", t => t.OptionID, cascadeDelete: true)
                .Index(t => t.Exam_QuestionID)
                .Index(t => t.OptionID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AttemptedOptions", "OptionID", "dbo.Options");
            DropForeignKey("dbo.AttemptedOptions", "Exam_QuestionID", "dbo.Exam_Question");
            DropIndex("dbo.AttemptedOptions", new[] { "OptionID" });
            DropIndex("dbo.AttemptedOptions", new[] { "Exam_QuestionID" });
            DropTable("dbo.AttemptedOptions");
        }
    }
}
