namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "MobileNum", c => c.String());
            DropColumn("dbo.Users", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Name", c => c.String());
            DropColumn("dbo.Users", "MobileNum");
        }
    }
}
