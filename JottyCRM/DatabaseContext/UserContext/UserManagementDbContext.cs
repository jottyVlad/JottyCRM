using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JottyCRM.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = System.Data.Entity.DbContext;

namespace JottyCRM.DatabaseContext.UserContext
{
    internal class UserManagementDbContext : DbContext
    {
        public System.Data.Entity.DbSet<User> Users { get; set; }

        public UserManagementDbContext() : base("DBConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.AddFromAssembly(Assembly.GetAssembly(typeof(UserManagementDbContext)));
        }
    }
}
