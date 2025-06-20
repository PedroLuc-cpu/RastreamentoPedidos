using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedidos.API.Data.Map.Entrega
{
    public class RotaMapping : IEntityTypeConfiguration<Rota>
    {


        public void Configure(EntityTypeBuilder<Rota> builder)
        {
            builder.ToTable("rotas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseSerialColumn()
                .HasColumnName("idRota");
            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar")
                .IsRequired();

            builder.Property(x => x.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(100)")
                .IsRequired();
            builder.HasMany(x => x.PontosParada)
                .WithOne()
                .HasForeignKey(x => x.IdRota)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Rota_PontoParada");
        }
    }
}
