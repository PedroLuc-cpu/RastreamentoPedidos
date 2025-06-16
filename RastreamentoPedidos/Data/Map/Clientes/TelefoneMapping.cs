using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.Data.Map.Clientes
{
    public class TelefoneMapping : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("telefone");
            builder.HasKey(t => t.IdTelefoneCliente);
            builder.Property(c => c.IdTelefoneCliente).UseSerialColumn().HasColumnName("idTelefoneCliente");
            builder.Property(t => t.Prefixo).HasColumnName("prefixo").HasColumnType("varchar");
            builder.Property(t => t.Numero).HasColumnName("numero").HasColumnType("varchar");
            builder.Property(t => t.Padrao).HasColumnName("padrao").HasDefaultValue("false");
            builder.HasOne<Cliente>()
                .WithMany(c => c.Telefones)
                .HasForeignKey(t => t.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
