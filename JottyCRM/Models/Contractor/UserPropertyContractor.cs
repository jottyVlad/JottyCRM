using System.Collections.Generic;

namespace JottyCRM.Models.Contractor
{
    public class UserPropertyContractor
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        
        public int UserTableId { get; set; }
        public User User { get; set; }
        
        public List<UserPropertyContractorValueToBase> Values { get; set; }
    }
}