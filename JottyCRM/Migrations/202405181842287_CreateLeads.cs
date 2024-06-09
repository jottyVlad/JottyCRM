using System.Runtime.Serialization;

namespace JottyCRM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateLeads : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Leads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Name = c.String(),
                        Status = c.String(),
                        Description = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserPropertyLeads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyName = c.String(),
                        UserTableId = c.Int(nullable: false),
                        User_Id = c.Int(),
                        Lead_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Leads", t => t.Lead_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Lead_Id);
            
            CreateTable(
                "dbo.UserPropertyLeadValueToBases",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PropertyId = c.Int(nullable: false),
                        Value = c.String(),
                        LeadId = c.Int(nullable: false),
                        UserPropertyLead_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leads", t => t.LeadId, cascadeDelete: true)
                .ForeignKey("dbo.UserPropertyLeads", t => t.UserPropertyLead_Id)
                .Index(t => t.LeadId)
                .Index(t => t.UserPropertyLead_Id);
            
            CreateTable(
                "dbo.Sells",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ContractorId = c.Int(nullable: false),
                        Name = c.String(),
                        SellDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Contractors", t => t.ContractorId, cascadeDelete: false)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: false)
                .Index(t => t.UserId)
                .Index(t => t.ContractorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sells", "UserId", "dbo.Users");
            DropForeignKey("dbo.Sells", "ContractorId", "dbo.Contractors");
            DropForeignKey("dbo.Leads", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserPropertyLeads", "Lead_Id", "dbo.Leads");
            DropForeignKey("dbo.UserPropertyLeadValueToBases", "UserPropertyLead_Id", "dbo.UserPropertyLeads");
            DropForeignKey("dbo.UserPropertyLeadValueToBases", "LeadId", "dbo.Leads");
            DropForeignKey("dbo.UserPropertyLeads", "User_Id", "dbo.Users");
            DropIndex("dbo.Sells", new[] { "ContractorId" });
            DropIndex("dbo.Sells", new[] { "UserId" });
            DropIndex("dbo.UserPropertyLeadValueToBases", new[] { "UserPropertyLead_Id" });
            DropIndex("dbo.UserPropertyLeadValueToBases", new[] { "LeadId" });
            DropIndex("dbo.UserPropertyLeads", new[] { "Lead_Id" });
            DropIndex("dbo.UserPropertyLeads", new[] { "User_Id" });
            DropIndex("dbo.Leads", new[] { "UserId" });
            DropTable("dbo.Sells");
            DropTable("dbo.UserPropertyLeadValueToBases");
            DropTable("dbo.UserPropertyLeads");
            DropTable("dbo.Leads");
        }
    }
}
