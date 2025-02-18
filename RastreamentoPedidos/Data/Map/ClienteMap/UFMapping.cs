using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedidos.Model.Clientes;

namespace RastreamentoPedidos.Data.Map.ClienteMap
{
    public class UFMapping : IEntityTypeConfiguration<UF>
    {
        public void Configure(EntityTypeBuilder<UF> builder)
        {
            builder.ToTable("uf");
            builder.HasKey(uf => uf.ifUF);
            builder.Property(uf => uf.ifUF).UseSerialColumn().HasColumnName("ifUF");
            builder.Property(uf => uf.sigla).HasColumnName("sigla").HasColumnType("varchar");
        }
    }
}
