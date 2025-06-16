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
            builder.HasKey(c => c.IdCidade);
            builder.Property(c => c.IdCidade).UseSerialColumn().HasColumnName("idCidade");
            builder.Property(c => c.Nome).HasColumnName("nome").HasColumnType("varchar");
            builder.Property(c => c.IdUF).HasColumnName("idUF").IsRequired();
            builder.HasOne(c => c.UF)
                .WithMany()
                .HasForeignKey(c => c.IdUF)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
