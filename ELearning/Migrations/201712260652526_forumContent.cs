namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class forumContent : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumPostReplies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Answer = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ForumPost_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.ForumPosts", t => t.ForumPost_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ForumPost_ID);
            
            CreateTable(
                "dbo.ForumPosts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Question = c.String(),
                        Discription = c.String(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Forum_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Fora", t => t.Forum_ID)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Forum_ID);
            
            AddColumn("dbo.Fora", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Fora", "ApplicationUser_Id");
            AddForeignKey("dbo.Fora", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumPostReplies", "ForumPost_ID", "dbo.ForumPosts");
            DropForeignKey("dbo.ForumPosts", "Forum_ID", "dbo.Fora");
            DropForeignKey("dbo.ForumPosts", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ForumPostReplies", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Fora", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ForumPosts", new[] { "Forum_ID" });
            DropIndex("dbo.ForumPosts", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ForumPostReplies", new[] { "ForumPost_ID" });
            DropIndex("dbo.ForumPostReplies", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Fora", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.Fora", "ApplicationUser_Id");
            DropTable("dbo.ForumPosts");
            DropTable("dbo.ForumPostReplies");
        }
    }
}
