namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sectionTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Links", "SectionTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Links", "SectionTitle");
        }
    }
}
