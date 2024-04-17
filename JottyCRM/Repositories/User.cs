using JottyCRM.DatabaseContext.UserContext;
using JottyCRM.Models;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JottyCRM.repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByLoginAsync(string login);
        User Create(User user);
    }

    public class UserRepository : IUserRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private UserManagementDbContext DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<UserManagementDbContext>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type UserManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                return dbContext;
            }
        }

        public UserRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if(ambientDbContextLocator == null) 
                throw new ArgumentNullException(nameof(ambientDbContextLocator));
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            
            try
            {
                User _user = await DbContext.Users.FirstAsync(predicate: (user) => user.Login == login);
                return _user;
            }
            catch
            {
                return null;
            }
        }

        public User Create(User user)
        {
            try
            {
                DbContext.Users.Add(user);
                return user;
            }
            catch
            {
                return null;
            }
        }
    }
}
