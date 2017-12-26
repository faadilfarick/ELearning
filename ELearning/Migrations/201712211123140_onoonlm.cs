namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class onoonlm : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Fora", "Course_ID", "dbo.Courses");
            DropIndex("dbo.Fora", new[] { "Course_ID" });
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Fora");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Fora",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseTitle = c.String(),
                        Description = c.String(),
                        Course_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.Replies");
            DropTable("dbo.Posts");
            CreateIndex("dbo.Fora", "Course_ID");
            AddForeignKey("dbo.Fora", "Course_ID", "dbo.Courses", "ID");
        }
    }
}
