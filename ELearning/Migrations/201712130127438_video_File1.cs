namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class video_File1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "FilePath", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "FilePath");
        }
    }
}
