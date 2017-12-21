namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class formAndCourseLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Fora", "Course_ID", c => c.Int());
            CreateIndex("dbo.Fora", "Course_ID");
            AddForeignKey("dbo.Fora", "Course_ID", "dbo.Courses", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fora", "Course_ID", "dbo.Courses");
            DropIndex("dbo.Fora", new[] { "Course_ID" });
            DropColumn("dbo.Fora", "Course_ID");
        }
    }
}
