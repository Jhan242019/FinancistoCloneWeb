using FinancistoCloneWebV2.Models.Maps;
using Microsoft.EntityFrameworkCore;

namespace FinancistoCloneWebV2.Models
{
    public class FinancistoContext: DbContext
    {
        // Constructor que invoca a su constructor padre
        public FinancistoContext(DbContextOptions<FinancistoContext> options)
            : base(options)
        {

        }

        //Propiedades
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        // Sobreescribir método
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // pasar la isntancia para hacer uso de la configuración de cada clase
            modelBuilder.ApplyConfiguration(new AccountMap());
            modelBuilder.ApplyConfiguration(new ProductoMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
        }
    }
}
