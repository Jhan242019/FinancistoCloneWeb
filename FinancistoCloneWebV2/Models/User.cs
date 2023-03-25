using System.ComponentModel.DataAnnotations;

namespace FinancistoCloneWebV2.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Ingrese un nombre de Usuario para crear una cuenta")]
        public string Username { get; set; }
        [Required(ErrorMessage = "La Contraseña es Obligatorio")]
        public string Password { get; set; }
    }
}
