using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedidos.Model;

namespace RastreamentoPedidos.Data
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("clientes").HasKey(x => x.id_cliente);
            builder.Property(x => x.id_cliente).HasColumnType("integer").UseSerialColumn();
            builder.Property(x => x.email).HasColumnName("email").HasColumnType("varchar");
            builder.Property(x => x.nome).HasColumnName("nome").HasColumnType("varchar");
            builder.Property(x => x.telefone).HasColumnName("telefone").HasColumnType("varchar");
            builder.HasMany(x => x.encomendas)
                .WithOne(e => e.cliente)
                .HasForeignKey(e => e.id_cliente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
