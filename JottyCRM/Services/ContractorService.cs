using System;
using System.Collections.Generic;
using JottyCRM.Models;
using JottyCRM.Models.Contractor;
using JottyCRM.repositories;
using Mehdime.Entity;

namespace JottyCRM.Services
{
    public interface IContractorService
    {
        Contractor Create(string fullName, string email, string phoneNumber, User user);
        List<Contractor> GetAll(int userId);
    }
    
    public class ContractorService : IContractorService
    {
        private readonly IContractorRepository _repository;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        
        public ContractorService(IDbContextScopeFactory dbContextScopeFactory, IContractorRepository repository)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException(nameof(dbContextScopeFactory));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public List<Contractor> GetAll(int userId)
        {
            using (var dbContextScope = _dbContextScopeFactory.Create())
            {
                return _repository.GetAll(userId);
            }
        }

        public Contractor Create(string fullName, 
            string email, 
            string phoneNumber,
            User user)
        {
            if (fullName == null)
            {
                return null;
            }
            var contractor = new Contractor()
            {
                FullName = fullName,
                Email = email,
                PhoneNumber = phoneNumber,
                UserId = user.Id
            };
            using (var ctx = _dbContextScopeFactory.Create())
            {
                _repository.CreateContractor(contractor);
                ctx.SaveChanges();
            }
            return contractor;
        }
    }
}