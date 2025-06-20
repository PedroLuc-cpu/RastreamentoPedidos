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
                .HasColumnName("idEncomendaAuditoria");
            builder.Property(x => x.IdEncomenda)
                .HasColumnName("idEncomenda")
                .IsRequired();
            builder.Property(x => x.DataHoraEvento)
                .HasColumnName("dataHoraEvento")
                .HasColumnType("timestamp")
                .IsRequired();
            builder.Property(x => x.LocalOrigem)
                .HasColumnName("localOrigem")
                .HasColumnType("varchar(255)")
                .IsRequired();
            builder.Property(x => x.LocalDestino)
                .HasColumnName("localDestino")
                .HasColumnType("varchar(255)")
                .IsRequired();
            builder.Property(x => x.StatusEntregas)
                .HasColumnName("statusEntregas")
                .HasColumnType("varchar(50)")
                .IsRequired();
            builder.Property(x => x.StatusAtual)
                .HasColumnName("statusAtual")
                .HasColumnType("varchar(50)")
                .IsRequired();
            builder.Property(x => x.DescricaoEvento)
                .HasColumnName("descricaoEvento")
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
                .HasColumnName("dataRegistro")
                .HasColumnType("timestamp")
                .IsRequired();
            builder.HasOne<Encomendas>()
                .WithMany(e => e.EncomendaAuditorias)
                .HasForeignKey(e => e.IdEncomenda)
                .OnDelete(DeleteBehavior.Cascade);        }
    }
}
