namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addSectionUserIpp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "newPosterIp", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Links", "newPosterIp");
        }
    }
}
