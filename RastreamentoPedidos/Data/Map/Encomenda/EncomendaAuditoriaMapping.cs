using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedidos.API.Data.Map.Entrega
{
    public class EncomendaAuditoriaMapping : IEntityTypeConfiguration<EncomendaAuditoria>
    {
        public void Configure(EntityTypeBuilder<EncomendaAuditoria> builder)
        {
            builder.ToTable("encomenda_auditoria");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseSerialColumn()
                .HasColumnName("id_encomenda_auditoria");

            builder.Property(x => x.EncomendaId)
                .HasColumnName("id_encomenda")
                .IsRequired();

            builder.Property(x => x.DataHoraEvento)
                .HasColumnName("data_hora_evento")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(x => x.LocalOrigem)
                .HasColumnName("local_origem")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(x => x.LocalDestino)
                .HasColumnName("local_destino")
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(x => x.StatusEntrega)
                .HasColumnName("status_entregas")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.StatusAtual)
                .HasColumnName("status_atual")
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder.Property(x => x.DescricaoEvento)
                .HasColumnName("descricao_evento")
                .HasColumnType("text")
                .IsRequired();

            builder.Property(x => x.Responsavel)
                .HasColumnName("responsavel")
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder.Property(x => x.Observacoes)
                .HasColumnName("observacoes")
                .HasColumnType("text");

            builder.Property(x => x.DataRegistro)
                .HasColumnName("data_registro")
                .HasColumnType("timestamp")
                .IsRequired();

            builder.HasOne<Encomendas>()
                .WithMany(e => e.Auditorias)
                .HasForeignKey(e => e.EncomendaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
