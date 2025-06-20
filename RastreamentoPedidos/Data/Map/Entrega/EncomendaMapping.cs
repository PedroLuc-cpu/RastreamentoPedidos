using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedidos.API.Data.Map.Entrega
{
    public class EncomendaMapping : IEntityTypeConfiguration<Encomendas>
    {
        public void Configure(EntityTypeBuilder<Encomendas> builder)
        {
            builder.ToTable("encomendas");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .UseSerialColumn()
                .HasColumnName("idEncomenda");

            builder.Property(x => x.CodigoRastreamento)
                .HasColumnName("codigo_rastreamento")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar")
                .IsRequired();

            builder.Property(x => x.DataEncomenda)
                .HasColumnName("data_pedido")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .HasColumnName("data_criacao")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.DataPrevisao)
                .HasColumnName("data_previsao")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.IdCliente)
                .HasColumnName("id_cliente")
                .IsRequired();
            builder.HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.IdStatusEncomenda)
                .HasColumnName("id_status_encomenda")
                .HasColumnType("varchar(50)")
                .IsRequired();
            builder.HasOne(x => x.StatusEncomenda)
                .WithMany()
                .HasForeignKey(x => x.IdStatusEncomenda)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.IdRota)
                .HasColumnName("id_rota")
                .IsRequired();
            builder.HasOne(x => x.Rota)
                .WithMany()
                .HasForeignKey(x => x.IdRota)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(x => x.EncomendaAuditorias)
                .WithOne()
                .HasForeignKey(x => x.IdEncomenda)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(x => x.CodigoRastreamento)
                .IsUnique()
                .HasDatabaseName("IX_CodigoRastreamento");
        }
    }
}
