namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class initFirst : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Connections",
                c => new
                {
                    Tid = c.Long(nullable: false, identity: true),
                    nameFrom = c.String(maxLength: 256),
                    nameType = c.String(),
                    nameTo = c.String(),
                    Remark = c.String(),
                    tStatus = c.Int(nullable: false),
                    ClientIP = c.String(),
                    addDate = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Tid)
                .Index(t => t.nameFrom)
                .Index(t => t.addDate);

            CreateTable(
                "dbo.EntitysAll",
                c => new
                {
                    Tid = c.Long(nullable: false, identity: true),
                    name = c.String(maxLength: 256),
                    ttype = c.String(maxLength: 256),
                    Countries = c.String(maxLength: 256),
                    Source = c.String(),
                    Status = c.String(),
                    Address = c.String(),
                    CompanyType = c.String(),
                    Jurisdiction = c.String(),
                    Remark = c.String(),
                    tStatus = c.Int(nullable: false),
                    ClientIP = c.String(),
                    addDate = c.DateTime(nullable: false, defaultValueSql: "getdate()"),
                    UpdateDate = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.Tid)
                .Index(t => t.name, unique: true)
                .Index(t => t.ttype)
                .Index(t => t.Countries)
                .Index(t => t.addDate);

        }

        public override void Down()
        {
            DropIndex("dbo.EntitysAll", new[] { "addDate" });
            DropIndex("dbo.EntitysAll", new[] { "Countries" });
            DropIndex("dbo.EntitysAll", new[] { "ttype" });
            DropIndex("dbo.EntitysAll", new[] { "name" });
            DropIndex("dbo.Connections", new[] { "addDate" });
            DropIndex("dbo.Connections", new[] { "nameFrom" });
            DropTable("dbo.EntitysAll");
            DropTable("dbo.Connections");
        }
    }
}
