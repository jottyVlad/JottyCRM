using System;
using System.Collections.Generic;
using JottyCRM.Models;
using JottyCRM.Models.Sell;
using JottyCRM.repositories;
using Mehdime.Entity;

namespace JottyCRM.Services
{
    public interface ISellService
    {
        Sell Create(string name, int contractorId, DateTime sellDateTime, Decimal amountOfTransaction, User user);
        List<Sell> GetAll(int userId);
    }
    
    public class SellService : ISellService
    {
        private readonly ISellRepository _repository;
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        
        public SellService(IDbContextScopeFactory dbContextScopeFactory, ISellRepository repository)
        {
            _dbContextScopeFactory = dbContextScopeFactory ?? throw new ArgumentNullException(nameof(dbContextScopeFactory));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Sell Create(string name, int contractorId, DateTime sellDateTime, Decimal amountOfTransaction, User user)
        {
            if (name == null || amountOfTransaction == 0 || contractorId == 0)
                return null;

            var sell = new Sell()
            {
                Name = name,
                AmountOfTransaction = amountOfTransaction,
                ContractorId = contractorId,
                SellDateTime = sellDateTime,
                UserId = user.Id
            };
            
            using (var ctx = _dbContextScopeFactory.Create())
            {
                _repository.CreateSell(sell);
                ctx.SaveChanges();
            }

            return sell;
        }

        public List<Sell> GetAll(int userId)
        {
            using (var ctx = _dbContextScopeFactory.Create())
            {
                return _repository.GetAll(userId);
            }
        }
    }
}