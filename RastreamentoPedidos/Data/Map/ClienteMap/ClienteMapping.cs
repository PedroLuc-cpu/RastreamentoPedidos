using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.Data.Map.ClienteMap
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.idCliente);
            builder.Property(c => c.idCliente).UseSerialColumn().HasColumnName("idCliente");

            builder.Property(c => c.nome)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(c => c.documento)
                   .IsRequired()
                   .HasMaxLength(14);

            builder.HasMany(c => c.enderecos)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.telefones)
                   .WithOne()
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.encomendas)
                   .WithOne(e => e.cliente)
                   .HasForeignKey(e => e.idCliente)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("Clientes");
        }
    }
}
