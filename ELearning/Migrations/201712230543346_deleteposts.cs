namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteposts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posts", "CourseID", "dbo.Courses");
            DropIndex("dbo.Posts", new[] { "CourseID" });
            DropTable("dbo.Posts");
        }
        
        public override void Down()
        {
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
            
            CreateIndex("dbo.Posts", "CourseID");
            AddForeignKey("dbo.Posts", "CourseID", "dbo.Courses", "ID", cascadeDelete: true);
        }
    }
}
