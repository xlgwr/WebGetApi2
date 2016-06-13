namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPageForAll : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Connections", "getPage", c => c.Int(nullable: false));
            AddColumn("dbo.EntitysAll", "getPage", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntitysAll", "getPage");
            DropColumn("dbo.Connections", "getPage");
        }
    }
}
