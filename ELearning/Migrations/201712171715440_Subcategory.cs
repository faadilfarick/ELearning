namespace ELearning.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Subcategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SubCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Discription = c.String(),
                        Category_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.Category_ID)
                .Index(t => t.Category_ID);
            
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SubCategories", "Category_ID", "dbo.Categories");
            DropIndex("dbo.SubCategories", new[] { "Category_ID" });
            AlterColumn("dbo.Categories", "Name", c => c.String());
            DropTable("dbo.SubCategories");
        }
    }
}
