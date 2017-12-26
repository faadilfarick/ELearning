namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fewchangesk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Replies", "Posts_ID", "dbo.Posts");
            DropIndex("dbo.Replies", new[] { "Posts_ID" });
            DropTable("dbo.Replies");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Replies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostID = c.Int(nullable: false),
                        Description = c.String(),
                        Posts_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.Replies", "Posts_ID");
            AddForeignKey("dbo.Replies", "Posts_ID", "dbo.Posts", "ID");
        }
    }
}
