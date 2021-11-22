namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumsToUserTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "NationalID", c => c.String());
            AddColumn("dbo.Users", "Elgeha", c => c.String());
            AddColumn("dbo.Users", "JobTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "JobTitle");
            DropColumn("dbo.Users", "Elgeha");
            DropColumn("dbo.Users", "NationalID");
        }
    }
}
