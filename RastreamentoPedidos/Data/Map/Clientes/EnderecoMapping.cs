using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedidos.Data.Map.Clientes
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("endereco").HasKey(x => x.IdEnderecoCliente);

            builder.Property(x => x.IdEnderecoCliente).UseSerialColumn().HasColumnName("idEnderecoCliente");

            builder.Property(x => x.Bairro)
                .HasColumnName("bairro")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Numero)
                .HasColumnName("numero")
                .HasColumnType("varchar");

            builder.Property(x => x.Rua)
               .HasColumnType("varchar")
               .HasMaxLength(100);

            builder.Property(x => x.CEP)
                .HasColumnType("varchar");

            builder.HasOne(x => x.TpLogradouro)
                .WithMany()
                .HasForeignKey(x => x.IdTpLogradouro)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Cliente>()
                .WithMany(c => c.Enderecos)
                .HasForeignKey(x => x.IdCliente)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Encomendas>()
                 .WithMany()
                 .HasForeignKey(x => x.EncomendaId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
