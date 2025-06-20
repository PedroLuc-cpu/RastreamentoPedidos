using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedidos.API.Data.Map.Encomenda
{
    public class RotaMapping : IEntityTypeConfiguration<Rota>
    {


        public void Configure(EntityTypeBuilder<Rota> builder)
        {
            builder.ToTable("rotas");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseSerialColumn()
                .HasColumnName("id_rota");

            builder.Property(x => x.Nome)
                .HasColumnName("nome")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.HasMany(x => x.PontosParada)
                .WithOne(x => x.Rota)
                .HasForeignKey(x => x.RotaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
