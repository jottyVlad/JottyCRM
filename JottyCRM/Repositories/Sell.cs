using System;
using System.Collections.Generic;
using System.Linq;
using JottyCRM.DatabaseContext.ContractorContext;
using JottyCRM.DatabaseContext.SellContext;
using JottyCRM.Models.Contractor;
using JottyCRM.Models.Sell;
using Mehdime.Entity;

namespace JottyCRM.repositories
{
    public interface ISellRepository
    {
        Sell CreateSell(Sell sell);
        List<Sell> GetSellsByName(string name, int userId);
        List<Sell> GetAll(int userId);
    }
    
    public class SellRepository : ISellRepository
    {
        private readonly IAmbientDbContextLocator _ambientDbContextLocator;

        private SellManagementDbContext DbContext
        {
            get
            {
                var dbContext = _ambientDbContextLocator.Get<SellManagementDbContext>();

                if (dbContext == null)
                    throw new InvalidOperationException("No ambient DbContext of type SellManagementDbContext found. This means that this repository method has been called outside of the scope of a DbContextScope. A repository must only be accessed within the scope of a DbContextScope, which takes care of creating the DbContext instances that the repositories need and making them available as ambient contexts. This is what ensures that, for any given DbContext-derived type, the same instance is used throughout the duration of a business transaction. To fix this issue, use IDbContextScopeFactory in your top-level business logic service method to create a DbContextScope that wraps the entire business transaction that your service method implements. Then access this repository within that scope. Refer to the comments in the IDbContextScope.cs file for more details.");

                return dbContext;
            }
        }

        public SellRepository(IAmbientDbContextLocator ambientDbContextLocator)
        {
            if(ambientDbContextLocator == null) 
                throw new ArgumentNullException(nameof(ambientDbContextLocator));
            _ambientDbContextLocator = ambientDbContextLocator;
        }

        public List<Sell> GetAll(int userId)
        {
            return DbContext.Sells.Where(s => s.UserId == userId).ToList();
        }
        
        public Sell CreateSell(Sell sell)
        {
            try
            {
                sell = DbContext.Sells.Add(sell);
                return sell;
            }
            catch
            {
                return null;
            }
        }

        public List<Sell> GetSellsByName(string name, int userId)
        {
            var sells = DbContext.Sells.Where(s => s.Name.Contains(name) && s.UserId == userId).ToList();
            return sells;
        }
    }
}