namespace JottyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        FullName = c.String(),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPropertyContractors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(),
                        UserTableId = c.Int(nullable: false),
                        User_Id = c.Int(),
                        Contractor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Contractors", t => t.Contractor_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Contractor_Id);
            
            CreateTable(
                "dbo.UserPropertyContractorValueToBases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyId = c.Int(nullable: false),
                        Value = c.String(),
                        ContractorId = c.Int(nullable: false),
                        UserPropertyContractor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contractors", t => t.ContractorId, cascadeDelete: true)
                .ForeignKey("dbo.UserPropertyContractors", t => t.UserPropertyContractor_Id)
                .Index(t => t.ContractorId)
                .Index(t => t.UserPropertyContractor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Contractors", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserPropertyContractors", "Contractor_Id", "dbo.Contractors");
            DropForeignKey("dbo.UserPropertyContractorValueToBases", "UserPropertyContractor_Id", "dbo.UserPropertyContractors");
            DropForeignKey("dbo.UserPropertyContractorValueToBases", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.UserPropertyContractors", "User_Id", "dbo.Users");
            DropIndex("dbo.UserPropertyContractorValueToBases", new[] { "UserPropertyContractor_Id" });
            DropIndex("dbo.UserPropertyContractorValueToBases", new[] { "ContractorId" });
            DropIndex("dbo.UserPropertyContractors", new[] { "Contractor_Id" });
            DropIndex("dbo.UserPropertyContractors", new[] { "User_Id" });
            DropIndex("dbo.Contractors", new[] { "UserId" });
            DropTable("dbo.UserPropertyContractorValueToBases");
            DropTable("dbo.UserPropertyContractors");
            DropTable("dbo.Contractors");
        }
    }
}
