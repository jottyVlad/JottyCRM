using JottyCRM.Models;
using JottyCRM.Models.Contractor;
using System.Data.Entity;
using System.Reflection;
using JottyCRM.Models.Lead;
using JottyCRM.Models.Sell;

namespace JottyCRM.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contractor> Contractors { get; set; }
        public DbSet<Sell> Sells { get; set; }
        public DbSet<Lead> Leads { get; set; }
        
        public ApplicationDatabaseContext() : base("DBConnection") { }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(ApplicationDatabaseContext)));
        }
    }
}