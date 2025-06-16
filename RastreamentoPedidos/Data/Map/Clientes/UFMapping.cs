using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.Data.Map.Clientes
{
    public class UFMapping : IEntityTypeConfiguration<UF>
    {
        public void Configure(EntityTypeBuilder<UF> builder)
        {
            builder.ToTable("uf");
            builder.HasKey(uf => uf.IdUF);
            builder.Property(uf => uf.IdUF).UseSerialColumn().HasColumnName("idUf");
            builder.Property(uf => uf.Sigla).HasColumnName("sigla").HasColumnType("varchar");
        }
    }
}
