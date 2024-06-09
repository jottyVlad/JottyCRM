namespace JottyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAmountOfTransaction : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sells", "AmountOfTransaction", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sells", "AmountOfTransaction");
        }
    }
}
