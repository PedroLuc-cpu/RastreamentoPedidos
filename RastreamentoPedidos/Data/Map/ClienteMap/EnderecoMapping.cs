using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedidos.Model.Clientes;
using RastreamentoPedidos.Model.Encomenda;

namespace RastreamentoPedidos.Data.Map.ClienteMap
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("endereco").HasKey(x => x.idEnderecoCliente);

            builder.Property(x => x.idEnderecoCliente).UseSerialColumn().HasColumnName("idEnderecoCliente");

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
                .HasForeignKey("idTpLogradouro")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Cliente>()
                .WithMany(c => c.enderecos)
                .HasForeignKey(x => x.idCliente)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Encomendas>()
                 .WithMany()
                 .HasForeignKey(x => x.EncomendaId)
                 .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
