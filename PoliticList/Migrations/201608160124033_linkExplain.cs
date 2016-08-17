namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class linkExplain : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "LinkExplain", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Links", "LinkExplain");
        }
    }
}
