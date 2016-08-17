namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeAndPiccc : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FeedLinks", "FeedLinkTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FeedLinks", "FeedLinkTime", c => c.String());
        }
    }
}
