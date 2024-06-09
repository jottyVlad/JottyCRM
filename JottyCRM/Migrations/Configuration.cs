
using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace JottyCRM.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<JottyCRM.DatabaseContext.ApplicationDatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }
    } 
}