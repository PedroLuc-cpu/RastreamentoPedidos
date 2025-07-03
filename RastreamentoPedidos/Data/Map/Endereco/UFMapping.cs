using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Endereco;

namespace RastreamentoPedidos.API.Data.Map.Endereco
{
    public class UFMapping : IEntityTypeConfiguration<UF>
    {
        public void Configure(EntityTypeBuilder<UF> builder)
        {
            builder.ToTable("uf");
            builder.HasKey(uf => uf.IdUF);
            builder.Property(uf => uf.IdUF).UseSerialColumn().HasColumnName("idUf");
            builder.Property(uf => uf.Sigla).HasColumnName("sigla").HasColumnType("varchar");
            builder.Property(uf => uf.Nome).HasColumnName("nome").HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.HasOne(uf => uf.Pais)
                .WithMany()
                .HasForeignKey(uf => uf.IdPais)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
