namespace JottyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class todatetime2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leads", "CreatedAt", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leads", "CreatedAt", c => c.DateTime(nullable: false));
        }
    }
}
