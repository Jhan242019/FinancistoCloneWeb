using System;

namespace FinancistoCloneWebV2.Models
{
    public class Transaction
    {
        public int Id { get; set; } 
        public int CuentaId { get; set; }
        public string Tipo { get; set; }
        public DateTime FechaHora { get; set; }
        public string Motivo { get; set; }
        public decimal Monto { get; set; }

        public Account Account { get; set; }

    }
}
