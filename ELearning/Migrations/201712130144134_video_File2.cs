namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class video_File2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Videos", "FilePath", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "FilePath", c => c.String(nullable: false));
        }
    }
}
