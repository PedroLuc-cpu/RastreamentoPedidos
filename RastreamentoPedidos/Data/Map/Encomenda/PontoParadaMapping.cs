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
                .HasColumnName("id_ponto_parada");

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

            builder.Property(x => x.RotaId)
                .HasColumnName("id_rota")
                .IsRequired();

            builder.HasOne(x => x.Rota)
                .WithMany(x => x.PontosParada)
                .HasForeignKey(x => x.RotaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
