namespace GetPanamaDB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDormDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EntitysAll", "DormDate", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EntitysAll", "DormDate");
        }
    }
}
