using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.Data.Map.Clientes
{
    public class CidadeMapping : IEntityTypeConfiguration<Cidade>
    {
        public void Configure(EntityTypeBuilder<Cidade> builder)
        {
            builder.ToTable("cidade");
            builder.HasKey(c => c.idCidade);
            builder.Property(c => c.idCidade).UseSerialColumn().HasColumnName("idCidade");
            builder.Property(c => c.nome).HasColumnName("nome").HasColumnType("varchar");
            builder.Property(c => c.idUF).HasColumnName("idUF").IsRequired();
            builder.HasOne(c => c.UF)
                .WithMany()
                .HasForeignKey(c => c.idUF)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
