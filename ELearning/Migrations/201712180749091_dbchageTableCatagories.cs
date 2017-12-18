namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dbchageTableCatagories : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Course_ID1", "dbo.Courses");
            DropIndex("dbo.Categories", new[] { "Course_ID1" });
            DropColumn("dbo.Categories", "Course_ID1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Course_ID1", c => c.Int());
            CreateIndex("dbo.Categories", "Course_ID1");
            AddForeignKey("dbo.Categories", "Course_ID1", "dbo.Courses", "ID");
        }
    }
}
