using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedidos.Model;

namespace RastreamentoPedidos.Data
{
    public class StatusEntregaMapping : IEntityTypeConfiguration<StatusEntrega>
    {
        public void Configure(EntityTypeBuilder<StatusEntrega> builder)
        {
            builder.ToTable("status_entrega").HasKey(x => x.id_status_entrega);
            builder.Property(x => x.id_status_entrega).HasColumnType("integer").UseSerialColumn();
            builder.Property(x => x.status)
                .HasColumnName("status")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .HasColumnName("data_atualizacao")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.id_status_entrega)
                .HasColumnName("id_encomenda")
                .IsRequired();

            // Configuração do relacionamento com Encomenda (Muitos para Um)
            builder.HasOne(x => x.encomenda)
                .WithMany(e => e.statusEntregas)
                .HasForeignKey(x => x.id_status_entrega)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
