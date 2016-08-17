namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class voterFeedId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voters", "FeedLinkId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Voters", "FeedLinkId");
        }
    }
}
