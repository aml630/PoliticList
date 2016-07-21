namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Links",
                c => new
                    {
                        LinkId = c.Int(nullable: false, identity: true),
                        LinkTitle = c.String(),
                        LinkUrl = c.String(),
                        LinkQuote = c.String(),
                        LinkTwitter = c.String(),
                        Published = c.Boolean(nullable: false),
                        Votes = c.Int(nullable: false),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LinkId)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        TopicId = c.Int(nullable: false, identity: true),
                        TopicName = c.String(),
                        TopicSlug = c.String(),
                        TopicPic = c.String(),
                        TopicPublished = c.Boolean(nullable: false),
                        Intro = c.String(),
                        FbShares = c.Int(nullable: false),
                        TwitShares = c.Int(nullable: false),
                        Topic_TopicId = c.Int(),
                    })
                .PrimaryKey(t => t.TopicId)
                .ForeignKey("dbo.Topics", t => t.Topic_TopicId)
                .Index(t => t.Topic_TopicId);
            
            CreateTable(
                "dbo.Voters",
                c => new
                    {
                        VoterId = c.Int(nullable: false, identity: true),
                        VoterIPAddress = c.String(),
                        ArticleSegmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Links", "TopicId", "dbo.Topics");
            DropForeignKey("dbo.Topics", "Topic_TopicId", "dbo.Topics");
            DropIndex("dbo.Topics", new[] { "Topic_TopicId" });
            DropIndex("dbo.Links", new[] { "TopicId" });
            DropTable("dbo.Voters");
            DropTable("dbo.Topics");
            DropTable("dbo.Links");
        }
    }
}
