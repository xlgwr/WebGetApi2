namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addchangeIndexForCountIndexCountryItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountryItems", "CountriesIndex", c => c.Int(nullable: false));
            CreateIndex("dbo.CountryItems", "CountriesIndex");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CountryItems", new[] { "CountriesIndex" });
            DropColumn("dbo.CountryItems", "CountriesIndex");
        }
    }
}
