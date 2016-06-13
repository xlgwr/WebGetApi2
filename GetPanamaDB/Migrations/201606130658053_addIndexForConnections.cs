namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIndexForConnections : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Connections", "nameType", c => c.String(maxLength: 256));
            AlterColumn("dbo.Connections", "nameTo", c => c.String(maxLength: 256));
            CreateIndex("dbo.Connections", "nameType");
            CreateIndex("dbo.Connections", "nameTo");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Connections", new[] { "nameTo" });
            DropIndex("dbo.Connections", new[] { "nameType" });
            AlterColumn("dbo.Connections", "nameTo", c => c.String());
            AlterColumn("dbo.Connections", "nameType", c => c.String());
        }
    }
}
