namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newslinksource : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsLinks", "NewsLinkSource", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsLinks", "NewsLinkSource");
        }
    }
}
