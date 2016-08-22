namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class catParent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedLinks", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.FeedLinks", "CategoryId");
            AddForeignKey("dbo.FeedLinks", "CategoryId", "dbo.Categories", "CategoryId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeedLinks", "CategoryId", "dbo.Categories");
            DropIndex("dbo.FeedLinks", new[] { "CategoryId" });
            DropColumn("dbo.FeedLinks", "CategoryId");
        }
    }
}
