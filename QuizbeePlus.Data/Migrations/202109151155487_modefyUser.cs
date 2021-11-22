namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modefyUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "University", c => c.String());
            AddColumn("dbo.Users", "Faculty", c => c.String());
            AddColumn("dbo.Users", "Grade", c => c.String());
            DropColumn("dbo.Exams", "total");
            DropColumn("dbo.Categories", "ResultVisible");
            DropColumn("dbo.Categories", "Time");
            DropColumn("dbo.Categories", "IsEnglish");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "IsEnglish", c => c.Boolean(nullable: false));
            AddColumn("dbo.Categories", "Time", c => c.Int(nullable: false));
            AddColumn("dbo.Categories", "ResultVisible", c => c.Boolean(nullable: false));
            AddColumn("dbo.Exams", "total", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Users", "Grade");
            DropColumn("dbo.Users", "Faculty");
            DropColumn("dbo.Users", "University");
        }
    }
}
