using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.Data.Map.ClienteMap
{
    public class TpLogradouroMapping : IEntityTypeConfiguration<TpLogradouro>
    {
        public void Configure(EntityTypeBuilder<TpLogradouro> builder)
        {
            builder.ToTable("tp_logradouro");
            builder.HasKey(tp => tp.idTpLogradouro);
            builder.Property(c => c.idTpLogradouro).UseSerialColumn().HasColumnName("idTpLogradouro");
            builder.Property(tp => tp.nome).HasColumnName("nome").HasColumnType("varchar");
            builder.Property(tp => tp.sigla).HasColumnName("sigla").HasColumnType("varchar");

        }
    }
}
