using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedidos.Data.Map.Clientes
{
       public class ClienteMapping : IEntityTypeConfiguration<Cliente>
       {
              public void Configure(EntityTypeBuilder<Cliente> builder)
              {
                     builder.HasKey(c => c.IdCliente);
                     builder.Property(c => c.IdCliente).UseSerialColumn().HasColumnName("idCliente");

                     builder.Property(c => c.Nome)
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(c => c.Email)
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(c => c.Documento)
                            .IsRequired()
                            .HasMaxLength(14);

                     builder.HasMany(c => c.Enderecos)
                            .WithOne()
                            .OnDelete(DeleteBehavior.Cascade);

                     builder.HasMany(c => c.Telefones)
                            .WithOne()
                            .OnDelete(DeleteBehavior.Cascade);

                     builder.HasMany(c => c.Encomendas)
                            .WithOne(e => e.Cliente)
                            .HasForeignKey(e => e.ClienteId)
                            .OnDelete(DeleteBehavior.Cascade);
                    builder.HasOne(c => c.EstadoCivil)
                            .WithOne()
                            .HasForeignKey<EstadoCivil>(ec => ec.Id)
                            .OnDelete(DeleteBehavior.Cascade);

                    builder.ToTable("Clientes");
              }
       }
}
