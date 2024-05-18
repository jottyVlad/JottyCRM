using System;
using System.Collections.Generic;

namespace JottyCRM.Models.Lead
{
    public class Lead
    {
        public int Id { get; set; }
        
        public int UserId { get; set; }
        public User User { get; set; }
        
        public string Name { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } 
        
        public List<UserPropertyLead> AdditionalProperties { get; set; }
    }
}