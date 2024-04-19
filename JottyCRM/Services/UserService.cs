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
        UserRegistered Create(string name, string login, string email, string password);
    }
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;


        public UserService(IDbContextScopeFactory dbContextScopeFactory, IUserRepository repository)
        {
            if(dbContextScopeFactory == null) throw new ArgumentNullException(nameof(dbContextScopeFactory));
            if(repository == null) throw new ArgumentNullException(nameof(repository));

            _dbContextScopeFactory = dbContextScopeFactory;
            _repository = repository;
        }

        private static String sha256_hash(String value)
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
        
        public UserRegistered Create(string name, string login, string email, string password)
        {
            if (name == null || login == null || email == null || password == null)
            {
                return new UserRegistered(null, false, "Все поля должны быть заполнены!");
            }

            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                if (_repository.IsLoginExists(login))
                {
                    return new UserRegistered(null, false, "Пользователь с таким логином уже существует!");
                }

                if (_repository.IsEmailExists(email))
                {
                    return new UserRegistered(null, false, "Пользователь с таким email уже существует!");
                }

                string passwordHashed = sha256_hash(password);

                User user = new User()
                {
                    Name = name,
                    Login = login,
                    Email = email,
                    Password = passwordHashed
                };
                _repository.Create(user);
                dbContextScope.SaveChanges();
                return new UserRegistered(user, true, "");
            }
        }
    }

    public class UserAuthorized
    {
        public readonly User UserObject;
        public readonly bool StatusCode;

        public UserAuthorized(User user, bool status)
        {
            UserObject = user;
            StatusCode = status;
        }
    }

    public class UserRegistered : UserAuthorized
    {
        public readonly string ErrorMessage;

        public UserRegistered(User user, bool status, string errorMessage) : base(user, status)
        {
            ErrorMessage = errorMessage;
        }
    }
}
