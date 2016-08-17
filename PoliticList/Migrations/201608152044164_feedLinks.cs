namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class feedLinks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeedLinks",
                c => new
                    {
                        FeedLinkId = c.Int(nullable: false, identity: true),
                        FeedLinkTitle = c.String(),
                        FeedLinkUrl = c.String(),
                        Votes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.FeedLinkId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.FeedLinks");
        }
    }
}
