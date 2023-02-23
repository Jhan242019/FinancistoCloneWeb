using System.ComponentModel.DataAnnotations;

namespace FinancistoCloneWebV2.Models
{
    public class Account
    {
        public int Id { get; set; } 
        public string Type { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Name { get; set; }   
        public string Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }
    }
}
