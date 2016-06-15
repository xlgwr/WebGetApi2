namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUrlForAll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Connections", "nameFromURL", c => c.String());
            AddColumn("dbo.Connections", "nameToURL", c => c.String());
            AddColumn("dbo.EntitysAll", "nameURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntitysAll", "nameURL");
            DropColumn("dbo.Connections", "nameToURL");
            DropColumn("dbo.Connections", "nameFromURL");
        }
    }
}
