namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeSizeForEntity : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.EntitysAll", new[] { "ttype" });
            DropIndex("dbo.EntitysAll", new[] { "Countries" });
            AlterColumn("dbo.EntitysAll", "ttype", c => c.String(maxLength: 32));
            AlterColumn("dbo.EntitysAll", "Countries", c => c.String(maxLength: 32));
            CreateIndex("dbo.EntitysAll", "ttype");
            CreateIndex("dbo.EntitysAll", "Countries");
        }
        
        public override void Down()
        {
            DropIndex("dbo.EntitysAll", new[] { "Countries" });
            DropIndex("dbo.EntitysAll", new[] { "ttype" });
            AlterColumn("dbo.EntitysAll", "Countries", c => c.String(maxLength: 256));
            AlterColumn("dbo.EntitysAll", "ttype", c => c.String(maxLength: 256));
            CreateIndex("dbo.EntitysAll", "Countries");
            CreateIndex("dbo.EntitysAll", "ttype");
        }
    }
}
