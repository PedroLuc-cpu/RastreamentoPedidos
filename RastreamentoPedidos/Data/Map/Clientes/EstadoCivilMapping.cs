using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.API.Data.Map.Clientes
{
    public class EstadoCivilMapping : IEntityTypeConfiguration<EstadoCivil>
    {
        public void Configure(EntityTypeBuilder<EstadoCivil> builder)
        {
            builder.ToTable("estadoCivil");
            builder.HasKey(ec => ec.Id);
            builder.Property(ec => ec.Id)
                .UseSerialColumn()
                .HasColumnName("idestadocivil");
            builder.Property(ec => ec.EstadoCivilDescricao);
            builder.Property(ec => ec.IdCliente)
                .HasColumnName("idcliente");
            builder.HasOne<Cliente>()
                .WithOne()
                .HasForeignKey<EstadoCivil>(ec => ec.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
