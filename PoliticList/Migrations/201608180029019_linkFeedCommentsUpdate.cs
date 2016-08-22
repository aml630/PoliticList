namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkFeedCommentsUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Post = c.String(),
                        Author = c.String(),
                        IpAddress = c.String(),
                        Votes = c.Int(nullable: false),
                        FeedLinkId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Comments");
        }
    }
}
