using JottyCRM.DatabaseContext.UserContext;
using JottyCRM.Models;
using Mehdime.Entity;
using System;
using System.Linq;

namespace JottyCRM.repositories
{
    public interface IUserRepository
    {
        User GetUserByLogin(string login);
        User Create(User user);
        bool IsEmailExists(string email);
        bool IsLoginExists(string login);
        User ChangePassword(string newPasswordHash, User currentUser);
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
                    throw new InvalidOperationException("Не найден DbContext");

                return dbContext;
            }
        }

        public UserRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if(ambientDbContextLocator == null) 
                throw new ArgumentNullException(nameof(ambientDbContextLocator));
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public User GetUserByLogin(string login)
        {
            try
            {
                User _user = DbContext.Users.First(predicate: (user) => user.Login == login);
                return _user;
            }
            catch
            {
                return null;
            }
        }

        public bool IsEmailExists(string email) => DbContext.Users.Any(s => s.Email == email);
        public bool IsLoginExists(string login) => DbContext.Users.Any(s => s.Login == login);

        public User ChangePassword(string newPasswordHash, User currentUser)
        {
            var user = GetUserByLogin(currentUser.Login);
            user.Password = newPasswordHash;
            return user;
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
