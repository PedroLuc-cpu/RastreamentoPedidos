using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedidos.Model.Encomenda;

namespace RastreamentoPedidos.Data.Map
{
    public class EncomendaMapping : IEntityTypeConfiguration<Encomendas>
    {
        public void Configure(EntityTypeBuilder<Encomendas> builder)
        {

            builder.ToTable("encomendas").HasKey(x => x.IdEncomenda);
            builder.Property(x => x.IdEncomenda).UseSerialColumn().HasColumnName("idEncomenda");
            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar");

            builder.Property(x => x.DataEncomenda)
                .HasColumnName("data_pedido")
                .HasColumnType("timestamp");

            builder.HasMany(x => x.StatusEntregas)
                .WithOne()
                .HasForeignKey("id_encomenda")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Localizacao)
                .WithOne()
                .HasForeignKey("EncomendaId")
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Cliente>()
                .WithMany(c => c.Encomendas)
                .HasForeignKey(x => x.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
