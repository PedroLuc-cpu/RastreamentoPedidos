using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Model.Encomenda;
using RastreamentoPedido.Core.Model.Endereco;

namespace RastreamentoPedidos.API.Data.Map.Endereco
{
    public class EnderecoMapping : IEntityTypeConfiguration<Enderecos>
    {
        public void Configure(EntityTypeBuilder<Enderecos> builder)
        {
            builder.ToTable("endereco").HasKey(x => x.Id);

            builder.Property(x => x.Id).UseSerialColumn().HasColumnName("idEnderecoCliente");

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

            builder.Property(x => x.IdTpLogradouro).HasColumnName("idTpLogradouro").IsRequired();
            builder.HasOne(x => x.TpLogradouro)
                .WithMany()
                .HasForeignKey(x => x.IdTpLogradouro)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.IdEncomenda).HasColumnName("idEncomenda");
            builder.HasOne<Encomendas>()
                 .WithMany()
                 .HasForeignKey(x => x.IdEncomenda)
                 .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.IdPessoa).HasColumnName("idPessoa").IsRequired();
            builder.HasOne<Cliente>()
                .WithMany(c => c.Enderecos)
                .HasForeignKey(x => x.IdPessoa)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.IdCidade).HasColumnName("idCidade").IsRequired();
            builder.HasOne<Cliente>()
                .WithMany(c => c.Enderecos)
                .HasForeignKey(x => x.IdCidade)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
