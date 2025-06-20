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
            builder.HasKey(x => x.Codigo);
            builder.Property(x => x.Codigo)
                .UseSerialColumn()
                .HasColumnName("id");
            builder.Property(x => x.Status)
                .HasColumnName("status")
                .HasColumnType("varchar(100)")
                .IsRequired();
        }
    }
}
