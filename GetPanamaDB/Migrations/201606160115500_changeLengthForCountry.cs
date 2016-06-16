namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeLengthForCountry : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EntitysAll", new[] { "Countries" });
            AlterColumn("dbo.EntitysAll", "Countries", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EntitysAll", "Countries", c => c.String(maxLength: 128));
            CreateIndex("dbo.EntitysAll", "Countries");
        }
    }
}
