namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchageTableCatAndCou : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Course_ID", "dbo.Courses");
            DropIndex("dbo.Categories", new[] { "Course_ID" });
            DropColumn("dbo.Categories", "Course_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Course_ID", c => c.Int());
            CreateIndex("dbo.Categories", "Course_ID");
            AddForeignKey("dbo.Categories", "Course_ID", "dbo.Courses", "ID");
        }
    }
}
