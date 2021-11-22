namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addResultInExam : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Exams", "Result", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Exams", "Result");
        }
    }
}
