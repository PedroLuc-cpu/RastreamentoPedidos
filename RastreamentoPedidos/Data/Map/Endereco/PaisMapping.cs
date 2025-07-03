using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Endereco;

namespace RastreamentoPedidos.API.Data.Map.Endereco
{
    public class PaisMapping : IEntityTypeConfiguration<Pais>
    {
        public void Configure(EntityTypeBuilder<Pais> builder)
        {
            builder.ToTable("pais");
            builder.HasKey(p => p.IdPais);
            builder.Property(p => p.IdPais)
                .UseSerialColumn()
                .HasColumnName("idPais");
            builder.Property(p => p.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(p => p.Sigla)
                .HasColumnName("sigla")
                .HasColumnType("varchar")
                .HasMaxLength(3)
                .IsRequired();
            builder.Property(p => p.Cod_bcb)
                .HasColumnName("cod_bcb")
                .HasColumnType("varchar");
        }
    }
}
