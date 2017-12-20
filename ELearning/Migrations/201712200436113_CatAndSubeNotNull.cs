namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CatAndSubeNotNull : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "MainCategory_ID", "dbo.Categories");
            DropForeignKey("dbo.Courses", "SubCategory_ID", "dbo.SubCategories");
            DropIndex("dbo.Courses", new[] { "MainCategory_ID" });
            DropIndex("dbo.Courses", new[] { "SubCategory_ID" });
            AlterColumn("dbo.Courses", "MainCategory_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Courses", "SubCategory_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Courses", "MainCategory_ID");
            CreateIndex("dbo.Courses", "SubCategory_ID");
            AddForeignKey("dbo.Courses", "MainCategory_ID", "dbo.Categories", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Courses", "SubCategory_ID", "dbo.SubCategories", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "SubCategory_ID", "dbo.SubCategories");
            DropForeignKey("dbo.Courses", "MainCategory_ID", "dbo.Categories");
            DropIndex("dbo.Courses", new[] { "SubCategory_ID" });
            DropIndex("dbo.Courses", new[] { "MainCategory_ID" });
            AlterColumn("dbo.Courses", "SubCategory_ID", c => c.Int());
            AlterColumn("dbo.Courses", "MainCategory_ID", c => c.Int());
            CreateIndex("dbo.Courses", "SubCategory_ID");
            CreateIndex("dbo.Courses", "MainCategory_ID");
            AddForeignKey("dbo.Courses", "SubCategory_ID", "dbo.SubCategories", "ID");
            AddForeignKey("dbo.Courses", "MainCategory_ID", "dbo.Categories", "ID");
        }
    }
}
