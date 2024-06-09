using System.Data.Entity;
using System.Reflection;
using JottyCRM.Models.Sell;

namespace JottyCRM.DatabaseContext.SellContext
{
    public class SellManagementDbContext : DbContext
    {
        public DbSet<Sell> Sells { get; set; }

        public SellManagementDbContext() : base("DBConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(SellManagementDbContext)));
        }
    }
}