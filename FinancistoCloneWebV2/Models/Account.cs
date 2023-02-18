namespace FinancistoCloneWebV2.Models
{
    public class Account
    {
        public int Id { get; set; } 
        public string Type { get; set; }   
        public string Name { get; set; }   
        public string Currency { get; set; }
        public decimal Amount { get; set; }
    }
}
