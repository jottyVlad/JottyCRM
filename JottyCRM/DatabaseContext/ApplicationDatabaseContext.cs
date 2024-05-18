using JottyCRM.Models;
using JottyCRM.Models.Contractor;
using System.Data.Entity;
using System.Reflection;
using JottyCRM.DatabaseContext.ContractorContext;
using JottyCRM.Models.Lead;
using JottyCRM.Models.Sell;

namespace JottyCRM.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {
        public System.Data.Entity.DbSet<User> Users { get; set; }
        public System.Data.Entity.DbSet<Contractor> Contractors { get; set; }
        public System.Data.Entity.DbSet<Sell> Sells { get; set; }
        public System.Data.Entity.DbSet<Lead> Leads { get; set; }
        
        public ApplicationDatabaseContext() : base("DBConnection") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(ApplicationDatabaseContext)));
        }
    }
}