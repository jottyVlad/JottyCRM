using System.Data.Entity;
using System.Reflection;
using JottyCRM.Models.Lead;

namespace JottyCRM.DatabaseContext.LeadContext
{
    public class LeadManagementDbContext : DbContext
    {
        public DbSet<Lead> Leads { get; set; }

        public LeadManagementDbContext() : base("DBConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(LeadManagementDbContext)));
        }
    }
}