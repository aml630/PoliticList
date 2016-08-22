namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkFeedComments : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedLinks", "FeedLink_FeedLinkId", c => c.Int());
            CreateIndex("dbo.FeedLinks", "FeedLink_FeedLinkId");
            AddForeignKey("dbo.FeedLinks", "FeedLink_FeedLinkId", "dbo.FeedLinks", "FeedLinkId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeedLinks", "FeedLink_FeedLinkId", "dbo.FeedLinks");
            DropIndex("dbo.FeedLinks", new[] { "FeedLink_FeedLinkId" });
            DropColumn("dbo.FeedLinks", "FeedLink_FeedLinkId");
        }
    }
}
