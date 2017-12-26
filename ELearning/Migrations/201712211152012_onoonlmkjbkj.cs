namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class onoonlmkjbkj : DbMigration
    {
        public override void Up()
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .Index(t => t.Course_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Fora", "Course_ID", "dbo.Courses");
            DropIndex("dbo.Fora", new[] { "Course_ID" });
            DropTable("dbo.Fora");
        }
    }
}
