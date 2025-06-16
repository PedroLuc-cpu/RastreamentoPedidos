using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model;

namespace RastreamentoPedidos.Data.Map
{
    public class StatusEntregaMapping : IEntityTypeConfiguration<StatusEntrega>
    {
        public void Configure(EntityTypeBuilder<StatusEntrega> builder)
        {
            builder.ToTable("status_entrega").HasKey(x => x.Codigo);
            builder.Property(x => x.Codigo).UseSerialColumn();
            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasColumnType("varchar")
                .HasMaxLength(225)
                .IsRequired();

            builder.Property(x => x.Timestamp)
                .HasColumnName("data_atualizacao")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.Codigo)
                .HasColumnName("id_encomenda")
                .IsRequired();

            // Configuração do relacionamento com Encomenda (Muitos para Um)
            builder.HasOne(x => x.Encomenda)
                .WithMany(e => e.StatusEntregas)
                .HasForeignKey(x => x.Codigo)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
