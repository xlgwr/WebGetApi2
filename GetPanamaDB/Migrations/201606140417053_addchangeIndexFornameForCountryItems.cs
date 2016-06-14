namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addchangeIndexFornameForCountryItems : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CountryItems", new[] { "name" });
            CreateIndex("dbo.CountryItems", "name");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CountryItems", new[] { "name" });
            CreateIndex("dbo.CountryItems", "name", unique: true);
        }
    }
}
