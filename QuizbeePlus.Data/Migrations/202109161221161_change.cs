namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Name", c => c.String());
            AddColumn("dbo.Users", "BirthDate", c => c.DateTime());
            AddColumn("dbo.Users", "GradYear", c => c.String());
            AddColumn("dbo.Users", "Sector", c => c.String());
            AddColumn("dbo.Users", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "Address");
            DropColumn("dbo.Users", "Sector");
            DropColumn("dbo.Users", "GradYear");
            DropColumn("dbo.Users", "BirthDate");
            DropColumn("dbo.Users", "Name");
        }
    }
}
