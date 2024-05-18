using System.Collections.Generic;

namespace JottyCRM.Models.Contractor
{
    public class Contractor
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public List<UserPropertyContractor> AdditionalProperties { get; set; }
    }
}