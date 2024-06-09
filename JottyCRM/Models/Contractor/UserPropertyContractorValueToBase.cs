namespace JottyCRM.Models.Contractor
{
    public class UserPropertyContractorValueToBase
    {
        public int Id { get; set; }
        
        public int PropertyId { get; set; }
        public UserPropertyContractor UserPropertyContractor { get; set; }
        
        public string Value { get; set; }
        
        public int ContractorId { get; set; }
        public Models.Contractor.Contractor Contractor { get; set; }
    }
}