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

            builder.ToTable("encomendas").HasKey(x => x.id_encomenda);
            builder.Property(x => x.id_encomenda).UseSerialColumn().HasColumnName("idEncomenda");
            builder.Property(x => x.descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar");

            builder.Property(x => x.data_encomenda)
                .HasColumnName("data_pedido")
                .HasColumnType("timestamp");

            builder.HasMany(x => x.statusEntregas)
                .WithOne()
                .HasForeignKey("id_encomenda")
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.localizacao)
                .WithOne()
                .HasForeignKey("EncomendaId")
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne<Cliente>()
                .WithMany(c => c.encomendas)
                .HasForeignKey(x => x.idCliente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
