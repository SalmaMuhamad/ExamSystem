namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTblsAddedColHall_UC_Data : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Exams", name: "Category_ID", newName: "CategoryID");
            RenameIndex(table: "dbo.Exams", name: "IX_Category_ID", newName: "IX_CategoryID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Exams", name: "IX_CategoryID", newName: "IX_Category_ID");
            RenameColumn(table: "dbo.Exams", name: "CategoryID", newName: "Category_ID");
        }
    }
}
