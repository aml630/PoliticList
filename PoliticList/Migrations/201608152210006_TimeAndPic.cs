namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TimeAndPic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsLinks", "NewsLinkTime", c => c.String());
            AddColumn("dbo.NewsLinks", "NewsLinkPic", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsLinks", "NewsLinkPic");
            DropColumn("dbo.NewsLinks", "NewsLinkTime");
        }
    }
}
