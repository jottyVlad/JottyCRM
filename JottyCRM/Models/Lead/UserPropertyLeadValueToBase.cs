namespace JottyCRM.Models.Lead
{
    public class UserPropertyLeadValueToBase
    {
        public int Id { get; set; }
        
        public int PropertyId { get; set; }
        public UserPropertyLead UserPropertyLead { get; set; }
        
        public string Value { get; set; }
        
        public int LeadId { get; set; }
        public Lead Lead { get; set; }
    }
}