using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancistoCloneWebV2.Models.Maps
{
    public class TypeMap : IEntityTypeConfiguration<Types>
    {
        public void Configure(EntityTypeBuilder<Types> builder)
        {
            builder.ToTable("Types");
            builder.HasKey(o => o.Id);
        }
    }
}
