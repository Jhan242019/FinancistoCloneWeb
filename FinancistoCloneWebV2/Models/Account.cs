using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancistoCloneWebV2.Models
{
    public class Account
    {
        public int Id { get; set; } 
        public int TypeId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Name { get; set; }   
        public string Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }

        public string ImagePath { get; set; }
        public int UserId { get; set; }

        public Types Type { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}
