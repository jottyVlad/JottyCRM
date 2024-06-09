using System.Collections.Generic;
using JottyCRM.Models.Lead;

namespace JottyCRM.Models.Lead
{
    public class UserPropertyLead
    {
        public int Id { get; set; }
        public string PropertyName { get; set; }
        
        public int UserTableId { get; set; }
        public User User { get; set; }
        
        public List<UserPropertyLeadValueToBase> Values { get; set; }
    }
}