using JottyCRM.Models;
using JottyCRM.repositories;
using Mehdime.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JottyCRM.services
{
    public interface IUserService
    {
        Task<UserAuthorized> TryAuthorize(string login, string password);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;


        public UserService(IDbContextScopeFactory dbContextScopeFactory, IUserRepository repository)
        {
            if(dbContextScopeFactory == null) throw new ArgumentNullException(nameof(dbContextScopeFactory));
            if(repository == null) throw new ArgumentNullException(nameof(repository));

            this._dbContextScopeFactory = dbContextScopeFactory;
            this._repository = repository;
        }

        public static String sha256_hash(String value)
        {
            using (SHA256 hash = SHA256Managed.Create())
            {
                return String.Concat(hash
                  .ComputeHash(Encoding.UTF8.GetBytes(value))
                  .Select(item => item.ToString("x2")));
            }
        }

        public async Task<UserAuthorized> TryAuthorize(string login, string password)
        {
            if (String.IsNullOrEmpty(login))
            {
                return new UserAuthorized(null, false);
            }

            User _user;
            using(var dbContextScope = _dbContextScopeFactory.Create()) {
                _user = await _repository.GetUserByLoginAsync(login);
            }
            if(_user == null)
            {
                return new UserAuthorized(null, false);
            }

            string passwordFormHashed = sha256_hash(password);

            if (passwordFormHashed == _user.Password)
                return new UserAuthorized(_user, true);
            else
                return new UserAuthorized(null, false);
        }
    }

    public class UserAuthorized
    {
        public User UserObject { get; private set; }
        public bool StatusCode { get; private set; }

        public UserAuthorized(User user, bool status)
        {
            this.UserObject = user;
            this.StatusCode = status;
        }
    }
}
