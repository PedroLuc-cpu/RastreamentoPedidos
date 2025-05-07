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
            builder.HasKey(uf => uf.idUF);
            builder.Property(uf => uf.idUF).UseSerialColumn().HasColumnName("ifUF");
            builder.Property(uf => uf.sigla).HasColumnName("sigla").HasColumnType("varchar");
        }
    }
}
