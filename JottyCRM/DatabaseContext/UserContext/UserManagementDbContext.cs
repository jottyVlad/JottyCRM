using System.Data.Entity;
using System.Reflection;
using JottyCRM.Models;
using DbContext = System.Data.Entity.DbContext;

namespace JottyCRM.DatabaseContext.UserContext
{
    internal class UserManagementDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserManagementDbContext() : base("DBConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(UserManagementDbContext)));
        }
    }
}
