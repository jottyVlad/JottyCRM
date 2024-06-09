using System;

namespace JottyCRM.Models.Sell
{
    public class Sell
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public int ContractorId { get; set; }
        public Contractor.Contractor Contractor { get; set; }
        
        public string Name { get; set; }
        public DateTime SellDateTime { get; set; }
        
        public Decimal AmountOfTransaction { get; set; }
    }
}