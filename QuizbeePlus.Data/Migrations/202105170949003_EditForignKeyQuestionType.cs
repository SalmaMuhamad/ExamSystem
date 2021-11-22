namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditForignKeyQuestionType : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Questions", name: "Category_ID", newName: "CategoryID");
            RenameColumn(table: "dbo.Questions", name: "Degree_ID", newName: "DegreeID");
            RenameIndex(table: "dbo.Questions", name: "IX_Category_ID", newName: "IX_CategoryID");
            RenameIndex(table: "dbo.Questions", name: "IX_Degree_ID", newName: "IX_DegreeID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Questions", name: "IX_DegreeID", newName: "IX_Degree_ID");
            RenameIndex(table: "dbo.Questions", name: "IX_CategoryID", newName: "IX_Category_ID");
            RenameColumn(table: "dbo.Questions", name: "DegreeID", newName: "Degree_ID");
            RenameColumn(table: "dbo.Questions", name: "CategoryID", newName: "Category_ID");
        }
    }
}
