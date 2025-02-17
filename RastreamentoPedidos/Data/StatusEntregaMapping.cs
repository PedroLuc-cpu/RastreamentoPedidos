using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedidos.Model;

namespace RastreamentoPedidos.Data
{
    public class StatusEntregaMapping : IEntityTypeConfiguration<StatusEntrega>
    {
        public void Configure(EntityTypeBuilder<StatusEntrega> builder)
        {
            builder.ToTable("status_entrega").HasKey(x => x.codigo);
            builder.Property(x => x.codigo).HasColumnType("integer").UseSerialColumn();
            builder.Property(x => x.status)
                .HasColumnName("status")
                .HasColumnType("varchar")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .HasColumnName("data_atualizacao")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.codigo)
                .HasColumnName("id_encomenda")
                .IsRequired();

            // Configuração do relacionamento com Encomenda (Muitos para Um)
            builder.HasOne(x => x.encomenda)
                .WithMany(e => e.statusEntregas)
                .HasForeignKey(x => x.codigo)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
