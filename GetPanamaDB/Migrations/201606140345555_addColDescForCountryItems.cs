namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addColDescForCountryItems : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CountryItems", "CountriesDesc", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CountryItems", "CountriesDesc");
        }
    }
}
