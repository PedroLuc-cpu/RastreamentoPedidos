using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedidos.Model;

namespace RastreamentoPedidos.Data
{
    public class EncomendaMapping : IEntityTypeConfiguration<Encomenda>
    {
        public void Configure(EntityTypeBuilder<Encomenda> builder)
        {
            
            builder.ToTable("encomendas").HasKey(x => x.id_encomenda);
            builder.Property(x => x.id_encomenda).HasColumnType("integer").UseSerialColumn();
            builder.Property(x => x.descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.data_encomenda)
                .HasColumnName("data_pedido")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.id_cliente)
                .HasColumnName("id_cliente")
                .IsRequired();

            // Configuração do relacionamento com Cliente (Muitos para Um)
            builder.HasOne(x => x.cliente)
                .WithMany(c => c.encomendas)
                .HasForeignKey(x => x.id_cliente)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração do relacionamento com StatusEntrega (Um para Muitos)
            builder.HasMany(x => x.statusEntregas)
                .WithOne(s => s.encomenda)
                .HasForeignKey(s => s.codigo)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(e => e.localizacao)
                 .WithOne(e => e.encomenda)
                 .HasForeignKey(e => e.EncomendaId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
