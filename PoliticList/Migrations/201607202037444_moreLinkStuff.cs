namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreLinkStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "LinkInstagram", c => c.String());
            AddColumn("dbo.Links", "LinkImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Links", "LinkImage");
            DropColumn("dbo.Links", "LinkInstagram");
        }
    }
}
