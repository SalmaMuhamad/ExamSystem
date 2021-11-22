namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblAttemptedOption_Score : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exam_Question", "Score", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exam_Question", "Score");
        }
    }
}
