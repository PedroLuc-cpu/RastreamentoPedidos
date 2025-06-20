using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedidos.API.Data.Map.Entrega
{
    public class PontoParadaMapping : IEntityTypeConfiguration<PontoParada>
    {
        public void Configure(EntityTypeBuilder<PontoParada> builder)
        {
            builder.ToTable("pontos_parada");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseSerialColumn()
                .HasColumnName("idPontoParada");
            builder.Property(x => x.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.Property(x => x.Localizacao)
                .HasColumnName("localizacao")
                .HasColumnType("varchar(255)")
                .IsRequired();
            builder.Property(x => x.Ordem)
                .HasColumnName("ordem")
                .IsRequired();
            builder.HasOne(x => x.Rota)
                .WithMany(r => r.PontosParada)
                .HasForeignKey(x => x.IdRota)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
