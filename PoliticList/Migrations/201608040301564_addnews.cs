namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addnews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewsLinks",
                c => new
                    {
                        NewsLinkId = c.Int(nullable: false, identity: true),
                        NewsLinkTitle = c.String(),
                        NewsLinkUrl = c.String(),
                        TopicId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsLinkId)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.NewsLinks", "TopicId", "dbo.Topics");
            DropIndex("dbo.NewsLinks", new[] { "TopicId" });
            DropTable("dbo.NewsLinks");
        }
    }
}
