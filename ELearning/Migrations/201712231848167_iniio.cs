namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class iniio : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Replies", "Posts_ID", "dbo.Posts");
            DropIndex("dbo.Replies", new[] { "Posts_ID" });
            DropColumn("dbo.Replies", "Posts_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Replies", "Posts_ID", c => c.Int());
            CreateIndex("dbo.Replies", "Posts_ID");
            AddForeignKey("dbo.Replies", "Posts_ID", "dbo.Posts", "ID");
        }
    }
}
