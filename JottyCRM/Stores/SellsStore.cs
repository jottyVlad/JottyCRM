using System;
using JottyCRM.Models.Contractor;

namespace JottyCRM.Stores
{
    public class SellsStore
    {
        public event Action<string, int, DateTime, Decimal> SellCreated;

        public void CreateSell(string name, int contractorId, DateTime sellDateTime, Decimal amountOfTransaction)
        {
            SellCreated?.Invoke(name, contractorId, sellDateTime, amountOfTransaction);
        }
    }
}