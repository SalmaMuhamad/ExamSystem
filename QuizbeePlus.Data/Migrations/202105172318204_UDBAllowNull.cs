namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UDBAllowNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Exam_Question", "OptionID", "dbo.Options");
            DropIndex("dbo.Exam_Question", new[] { "OptionID" });
            AlterColumn("dbo.Exam_Question", "IsCorrect", c => c.Boolean());
            AlterColumn("dbo.Exam_Question", "OptionID", c => c.Int());
            CreateIndex("dbo.Exam_Question", "OptionID");
            AddForeignKey("dbo.Exam_Question", "OptionID", "dbo.Options", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Exam_Question", "OptionID", "dbo.Options");
            DropIndex("dbo.Exam_Question", new[] { "OptionID" });
            AlterColumn("dbo.Exam_Question", "OptionID", c => c.Int(nullable: false));
            AlterColumn("dbo.Exam_Question", "IsCorrect", c => c.Boolean(nullable: false));
            CreateIndex("dbo.Exam_Question", "OptionID");
            AddForeignKey("dbo.Exam_Question", "OptionID", "dbo.Options", "ID", cascadeDelete: true);
        }
    }
}
