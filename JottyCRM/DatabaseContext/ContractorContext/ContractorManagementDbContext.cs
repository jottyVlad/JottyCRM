using System.Data.Entity;
using System.Reflection;
using JottyCRM.Models;
using JottyCRM.Models.Contractor;

namespace JottyCRM.DatabaseContext.ContractorContext
{
    public class ContractorManagementDbContext : DbContext
    {
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<UserPropertyContractor> UserProperties { get; set; }
        public DbSet<UserPropertyContractorValueToBase> UserPropertyValues { get; set; }

        public ContractorManagementDbContext() : base("DBConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(ContractorManagementDbContext)));
        }
    }
}