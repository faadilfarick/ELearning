namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CourseAndVidesReqaddition : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "Discription", c => c.String(nullable: false));
            AlterColumn("dbo.Videos", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Videos", "Discription", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "Discription", c => c.String());
            AlterColumn("dbo.Videos", "Name", c => c.String());
            AlterColumn("dbo.Courses", "Discription", c => c.String());
            AlterColumn("dbo.Courses", "Name", c => c.String());
        }
    }
}
