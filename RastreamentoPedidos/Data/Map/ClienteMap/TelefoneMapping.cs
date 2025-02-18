using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedidos.Model.Clientes;

namespace RastreamentoPedidos.Data.Map.ClienteMap
{
    public class TelefoneMapping : IEntityTypeConfiguration<Telefone>
    {
        public void Configure(EntityTypeBuilder<Telefone> builder)
        {
            builder.ToTable("telefone");
            builder.HasKey(t => t.idTelefoneCliente);
            builder.Property(c => c.idTelefoneCliente).UseSerialColumn().HasColumnName("idTelefoneCliente");
            builder.Property(t => t.prefixo).HasColumnName("prefixo").HasColumnType("varchar");
            builder.Property(t => t.numero).HasColumnName("numero").HasColumnType("varchar");
            builder.Property(t => t.padrao).HasColumnName("padrao").HasDefaultValue("false");
            builder.HasOne<Cliente>()
                .WithMany(c => c.telefones)
                .HasForeignKey(t => t.idCliente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
