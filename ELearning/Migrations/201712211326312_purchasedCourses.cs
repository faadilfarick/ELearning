namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class purchasedCourses : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PurchasedCourses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Course_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Courses", t => t.Course_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Course_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PurchasedCourses", "Course_ID", "dbo.Courses");
            DropForeignKey("dbo.PurchasedCourses", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PurchasedCourses", new[] { "Course_ID" });
            DropIndex("dbo.PurchasedCourses", new[] { "ApplicationUser_Id" });
            DropTable("dbo.PurchasedCourses");
        }
    }
}
