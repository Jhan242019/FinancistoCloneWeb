using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinancistoCloneWebV2.Models
{
    public class Producto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string Nombre { get; set; }

        [Required]
        public int IdCategoria { get;set; }

        [Required(ErrorMessage = "El campo Proveedor es obligatorio")]
        public string Proveedor{ get;set; }

        [Required(ErrorMessage = "El campo Costo es obligatorio")]
        public decimal Costo{ get;set; }

        public Categoria Categoria { get;set; }
    }
}
