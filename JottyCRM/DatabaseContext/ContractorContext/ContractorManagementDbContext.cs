using System.Data.Entity;
using System.Reflection;
using JottyCRM.Models;
using JottyCRM.Models.Contractor;

namespace JottyCRM.DatabaseContext.ContractorContext
{
    public class ContractorManagementDbContext : DbContext
    {
        public System.Data.Entity.DbSet<Contractor> Contractors { get; set; }

        public ContractorManagementDbContext() : base("DBConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(ContractorManagementDbContext)));
        }
    }
}