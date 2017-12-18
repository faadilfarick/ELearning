namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchageTableSpellings : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Courses", name: "MianCategory_ID", newName: "MainCategory_ID");
            RenameIndex(table: "dbo.Courses", name: "IX_MianCategory_ID", newName: "IX_MainCategory_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Courses", name: "IX_MainCategory_ID", newName: "IX_MianCategory_ID");
            RenameColumn(table: "dbo.Courses", name: "MainCategory_ID", newName: "MianCategory_ID");
        }
    }
}
