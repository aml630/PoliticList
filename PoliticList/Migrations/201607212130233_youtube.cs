namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class youtube : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "YouTube", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "YouTube");
        }
    }
}
