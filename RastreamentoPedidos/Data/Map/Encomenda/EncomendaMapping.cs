using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedidos.API.Data.Map.Encomenda
{
    public class EncomendaMapping : IEntityTypeConfiguration<Encomendas>
    {
        public void Configure(EntityTypeBuilder<Encomendas> builder)
        {
            builder.ToTable("encomendas");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseSerialColumn()
                .HasColumnName("id_encomenda");

            builder.Property(x => x.CodigoRastreamento)
                .HasColumnName("codigo_rastreamento")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.Descricao)
                .HasColumnName("descricao")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(x => x.DataEncomenda)
                .HasColumnName("data_encomenda")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.DataCriacao)
                .HasColumnName("data_criacao")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.DataPrevisao)
                .HasColumnName("data_previsao")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.ClienteId)
                .HasColumnName("id_cliente")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder.HasOne(x => x.Cliente)
                .WithMany()
                .HasForeignKey(x => x.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.StatusEncomendaId)
                .HasColumnName("id_status_encomenda")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder.HasOne(x => x.StatusEncomenda)
                .WithMany(x => x.Encomendas)
                .HasForeignKey(x => x.StatusEncomendaId)
                .HasConstraintName("FK_encomendas_status_entregas_id")
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.RotaId)
                .HasColumnName("id_rota")
                .HasColumnType("INTEGER")
                .IsRequired();

            builder.HasOne(x => x.Rota)
                .WithMany()
                .HasForeignKey(x => x.RotaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Auditorias)
                .WithOne()
                .HasForeignKey(x => x.EncomendaId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => x.CodigoRastreamento)
                .IsUnique();
        }
    }
}
