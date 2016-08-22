namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VoterCommentId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Voters", "CommentId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Voters", "CommentId");
        }
    }
}
