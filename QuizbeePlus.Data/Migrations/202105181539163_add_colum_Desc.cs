namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_colum_Desc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Description");
        }
    }
}
