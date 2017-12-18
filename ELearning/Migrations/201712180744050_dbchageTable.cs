namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchageTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Course_ID", "dbo.Courses");
            AddColumn("dbo.Categories", "Course_ID1", c => c.Int());
            AddColumn("dbo.Courses", "MianCategory_ID", c => c.Int());
            AddColumn("dbo.Courses", "SubCategory_ID", c => c.Int());
            CreateIndex("dbo.Categories", "Course_ID1");
            CreateIndex("dbo.Courses", "MianCategory_ID");
            CreateIndex("dbo.Courses", "SubCategory_ID");
            AddForeignKey("dbo.Categories", "Course_ID", "dbo.Courses", "ID");
            AddForeignKey("dbo.Courses", "MianCategory_ID", "dbo.Categories", "ID");
            AddForeignKey("dbo.Courses", "SubCategory_ID", "dbo.SubCategories", "ID");
            AddForeignKey("dbo.Categories", "Course_ID1", "dbo.Courses", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "Course_ID1", "dbo.Courses");
            DropForeignKey("dbo.Courses", "SubCategory_ID", "dbo.SubCategories");
            DropForeignKey("dbo.Courses", "MianCategory_ID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "Course_ID", "dbo.Courses");
            DropIndex("dbo.Courses", new[] { "SubCategory_ID" });
            DropIndex("dbo.Courses", new[] { "MianCategory_ID" });
            DropIndex("dbo.Categories", new[] { "Course_ID1" });
            DropColumn("dbo.Courses", "SubCategory_ID");
            DropColumn("dbo.Courses", "MianCategory_ID");
            DropColumn("dbo.Categories", "Course_ID1");
            AddForeignKey("dbo.Categories", "Course_ID", "dbo.Courses", "ID");
        }
    }
}
