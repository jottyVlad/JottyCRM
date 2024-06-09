using System.Collections.Generic;
using JottyCRM.Models.Lead;

namespace JottyCRM.Models.Sell
{
    public class UserPropertySell
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        
        public int UserTableId { get; set; }
        public User User { get; set; }

        public List<UserPropertySellValueToBase> Values { get; set; }
    }
}