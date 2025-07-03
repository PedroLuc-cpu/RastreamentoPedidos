using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.API.Data.Map.Endereco
{
    public class TpLogradouroMapping : IEntityTypeConfiguration<TpLogradouro>
    {
        public void Configure(EntityTypeBuilder<TpLogradouro> builder)
        {
            builder.ToTable("tp_logradouro");
            builder.HasKey(tp => tp.IdTpLogradouro);
            builder.Property(c => c.IdTpLogradouro).UseSerialColumn().HasColumnName("idTpLogradouro");
            builder.Property(tp => tp.Nome).HasColumnName("nome").HasColumnType("varchar");
            builder.Property(tp => tp.Sigla).HasColumnName("sigla").HasColumnType("varchar");

        }
    }
}
