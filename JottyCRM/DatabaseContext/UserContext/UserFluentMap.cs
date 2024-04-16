using JottyCRM.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JottyCRM.DatabaseContext.UserContext
{
    internal class UserFluentMap : EntityTypeConfiguration<User>
    {
        public UserFluentMap() { 
            Property(u => u.Name).IsRequired();
            Property(u => u.Email).IsRequired();
            Property(u => u.Password).IsRequired();
            Property(u => u.Login).IsRequired();
        }
    }
}
