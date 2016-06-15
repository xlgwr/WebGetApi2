namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeLengForName : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Connections", new[] { "nameFrom" });
            DropIndex("dbo.Connections", new[] { "nameTo" });
            DropIndex("dbo.CountryItems", new[] { "Countries" });
            DropIndex("dbo.CountryItems", new[] { "name" });
            DropIndex("dbo.EntitysAll", new[] { "name" });
            DropIndex("dbo.EntitysAll", new[] { "Countries" });
            AlterColumn("dbo.Connections", "nameFrom", c => c.String(maxLength: 128));
            AlterColumn("dbo.Connections", "nameTo", c => c.String(maxLength: 128));
            AlterColumn("dbo.CountryItems", "Countries", c => c.String(maxLength: 128));
            AlterColumn("dbo.CountryItems", "name", c => c.String(maxLength: 128));
            AlterColumn("dbo.EntitysAll", "name", c => c.String(maxLength: 128));
            AlterColumn("dbo.EntitysAll", "Countries", c => c.String(maxLength: 128));
            CreateIndex("dbo.Connections", "nameFrom");
            CreateIndex("dbo.Connections", "nameTo");
            CreateIndex("dbo.CountryItems", "Countries");
            CreateIndex("dbo.CountryItems", "name");
            CreateIndex("dbo.EntitysAll", "name", unique: true);
            CreateIndex("dbo.EntitysAll", "Countries");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EntitysAll", new[] { "Countries" });
            DropIndex("dbo.EntitysAll", new[] { "name" });
            DropIndex("dbo.CountryItems", new[] { "name" });
            DropIndex("dbo.CountryItems", new[] { "Countries" });
            DropIndex("dbo.Connections", new[] { "nameTo" });
            DropIndex("dbo.Connections", new[] { "nameFrom" });
            AlterColumn("dbo.EntitysAll", "Countries", c => c.String(maxLength: 32));
            AlterColumn("dbo.EntitysAll", "name", c => c.String(maxLength: 32));
            AlterColumn("dbo.CountryItems", "name", c => c.String(maxLength: 32));
            AlterColumn("dbo.CountryItems", "Countries", c => c.String(maxLength: 32));
            AlterColumn("dbo.Connections", "nameTo", c => c.String(maxLength: 32));
            AlterColumn("dbo.Connections", "nameFrom", c => c.String(maxLength: 32));
            CreateIndex("dbo.EntitysAll", "Countries");
            CreateIndex("dbo.EntitysAll", "name", unique: true);
            CreateIndex("dbo.CountryItems", "name");
            CreateIndex("dbo.CountryItems", "Countries");
            CreateIndex("dbo.Connections", "nameTo");
            CreateIndex("dbo.Connections", "nameFrom");
        }
    }
}
