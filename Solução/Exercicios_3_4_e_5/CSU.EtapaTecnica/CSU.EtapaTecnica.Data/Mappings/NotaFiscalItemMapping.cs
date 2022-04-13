using CSU.EtapaTecnica.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSU.EtapaTecnica.Data.Mappings
{
    public class NotaFiscalItemMapping : IEntityTypeConfiguration<NotaFiscalItem>
    {
        public void Configure(EntityTypeBuilder<NotaFiscalItem> builder)
        {
            builder.ToTable("NOTAFISCALITENS");

            builder
                .HasKey(x => x.CodItem);

            builder
                .Property(x => x.CodItem)
                .IsRequired();

            builder
                .Property(x => x.DescrPro)
                .HasMaxLength(80);

            builder
                .Property(x => x.Unidade)
                .HasMaxLength(3);

            builder
                .Property(x => x.CodigoProdutoExterno)
                .HasMaxLength(20);
        }
    }
}
