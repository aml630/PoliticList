namespace PoliticList.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "Date", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "Date");
        }
    }
}
