namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkFeedCommentsUpdateAgain : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FeedLinks", "FeedLink_FeedLinkId", "dbo.FeedLinks");
            DropIndex("dbo.FeedLinks", new[] { "FeedLink_FeedLinkId" });
            CreateIndex("dbo.Comments", "FeedLinkId");
            AddForeignKey("dbo.Comments", "FeedLinkId", "dbo.FeedLinks", "FeedLinkId", cascadeDelete: true);
            DropColumn("dbo.FeedLinks", "FeedLink_FeedLinkId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FeedLinks", "FeedLink_FeedLinkId", c => c.Int());
            DropForeignKey("dbo.Comments", "FeedLinkId", "dbo.FeedLinks");
            DropIndex("dbo.Comments", new[] { "FeedLinkId" });
            CreateIndex("dbo.FeedLinks", "FeedLink_FeedLinkId");
            AddForeignKey("dbo.FeedLinks", "FeedLink_FeedLinkId", "dbo.FeedLinks", "FeedLinkId");
        }
    }
}
