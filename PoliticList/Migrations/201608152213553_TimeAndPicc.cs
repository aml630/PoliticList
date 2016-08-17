namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeAndPicc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FeedLinks", "FeedLinkTime", c => c.String());
            AddColumn("dbo.FeedLinks", "FeedLinkPic", c => c.String());
            DropColumn("dbo.NewsLinks", "NewsLinkTime");
            DropColumn("dbo.NewsLinks", "NewsLinkPic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NewsLinks", "NewsLinkPic", c => c.String());
            AddColumn("dbo.NewsLinks", "NewsLinkTime", c => c.String());
            DropColumn("dbo.FeedLinks", "FeedLinkPic");
            DropColumn("dbo.FeedLinks", "FeedLinkTime");
        }
    }
}
