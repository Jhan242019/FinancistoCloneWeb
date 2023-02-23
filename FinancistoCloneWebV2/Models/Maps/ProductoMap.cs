using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancistoCloneWebV2.Models.Maps
{
    public class ProductoMap : IEntityTypeConfiguration<Producto>
    {
        public void Configure(EntityTypeBuilder<Producto> builder)
        {
            builder.ToTable("Producto");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Categoria).WithMany().HasForeignKey(o => o.IdCategoria);
        }
    }
}
