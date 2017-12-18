namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryAdd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Discription = c.String(),
                        Course_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .Index(t => t.Course_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "Course_ID", "dbo.Courses");
            DropIndex("dbo.Categories", new[] { "Course_ID" });
            DropTable("dbo.Categories");
        }
    }
}
