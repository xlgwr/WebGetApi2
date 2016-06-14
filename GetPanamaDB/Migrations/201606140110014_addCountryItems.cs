namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class addCountryItems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryItems",
                c => new
                {
                    Tid = c.Long(nullable: false, identity: true),
                    Countries = c.String(maxLength: 32),
                    ttype = c.String(maxLength: 32),
                    name = c.String(maxLength: 32),
                    nameDesc = c.String(),
                    nameURL = c.String(),
                    Remark = c.String(),
                    getPage = c.Int(nullable: false),
                    tStatus = c.Int(nullable: false),
                    ClientIP = c.String(),
                    addDate = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Tid)
                .Index(t => t.Countries)
                .Index(t => t.ttype)
                .Index(t => t.name, unique: true)
                .Index(t => t.addDate);

        }

        public override void Down()
        {
            DropIndex("dbo.CountryItems", new[] { "addDate" });
            DropIndex("dbo.CountryItems", new[] { "name" });
            DropIndex("dbo.CountryItems", new[] { "ttype" });
            DropIndex("dbo.CountryItems", new[] { "Countries" });
            DropTable("dbo.CountryItems");
        }
    }
}
