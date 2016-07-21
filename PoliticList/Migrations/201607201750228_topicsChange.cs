namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class topicsChange : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Topics", "Topic_TopicId", "dbo.Topics");
            DropIndex("dbo.Topics", new[] { "Topic_TopicId" });
            DropColumn("dbo.Topics", "Topic_TopicId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Topics", "Topic_TopicId", c => c.Int());
            CreateIndex("dbo.Topics", "Topic_TopicId");
            AddForeignKey("dbo.Topics", "Topic_TopicId", "dbo.Topics", "TopicId");
        }
    }
}
