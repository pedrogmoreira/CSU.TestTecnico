using CSU.EtapaTecnica.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CSU.EtapaTecnica.Data.Mappings
{
    public class NotaFiscalMapping : IEntityTypeConfiguration<NotaFiscal>
    {
        public void Configure(EntityTypeBuilder<NotaFiscal> builder)
        {
            builder.ToTable("NOTA_FISCAL");

            builder
                .HasKey(x => x.CodNota);

            builder
                .Property(x => x.CodNota)
                .IsRequired();

            builder
                .Property(x => x.DestinatarioRemetente)
                .HasMaxLength(100);

            builder
                .Property(x => x.NumeroRecibo)
                .HasMaxLength(50);

            builder
                .HasMany(x => x.Produtos)
                .WithOne(u => u.NotaFiscal)
                .HasForeignKey(u => u.CodNota)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_NOTAFISCALITENS_NOTAFISCAL");

        }
    }
}
