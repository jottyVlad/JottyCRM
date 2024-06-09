namespace JottyCRM.Models.Sell
{
    public class UserPropertySellValueToBase
    {
        public int Id { get; set; }
        
        public int PropertyId { get; set; }
        public UserPropertySell UserPropertySell { get; set; }
        
        public string Value { get; set; }
        
        public int SellId { get; set; }
        public Lead.Lead Contractor { get; set; }
    }
}