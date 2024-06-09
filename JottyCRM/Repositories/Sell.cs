using System;
using System.Collections.Generic;
using System.Linq;
using JottyCRM.DatabaseContext.SellContext;
using JottyCRM.Models.Sell;
using Mehdime.Entity;
using DbFunctions = System.Data.Entity.DbFunctions;
using EntityState = System.Data.Entity.EntityState;

namespace JottyCRM.repositories
{
    public interface ISellRepository
    {
        Sell CreateSell(Sell sell);
        List<Sell> GetSellsByName(string name, int userId);
        List<Sell> GetAll(int userId);
        int GetCountOfSellsOnDate(DateTime dateTime, int userId);
        Decimal GetRevenueOnDate(DateTime dateTime, int userId);
        void DeleteSell(Sell sell);
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
                    throw new InvalidOperationException("Не найден DbContext");

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

        public int GetCountOfSellsOnDate(DateTime dateTime, int userId)
        {
            return DbContext.Sells.Count(s => DbFunctions.TruncateTime(s.SellDateTime) == dateTime.Date && s.UserId == userId);
        }

        public Decimal GetRevenueOnDate(DateTime dateTime, int userId)
        {
            var sells = DbContext.Sells.Where(s =>
                DbFunctions.TruncateTime(s.SellDateTime) == dateTime.Date && s.UserId == userId).ToList();
            return sells.Count == 0 ? 0 : sells.Sum(s => s.AmountOfTransaction);
        }

        public void DeleteSell(Sell sell)
        {
            DbContext.Entry(sell).State = EntityState.Deleted;
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