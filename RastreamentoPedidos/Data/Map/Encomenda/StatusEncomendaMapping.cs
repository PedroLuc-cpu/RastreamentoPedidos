using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedidos.API.Data.Map.Encomenda
{
    public class StatusEncomendaMapping : IEntityTypeConfiguration<StatusEncomenda>
    {
        public void Configure(EntityTypeBuilder<StatusEncomenda> builder)
        {
            builder.ToTable("status_entregas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseSerialColumn()
                .HasColumnName("id");
            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasColumnType("varchar")
                .IsRequired();
        }
    }
}
