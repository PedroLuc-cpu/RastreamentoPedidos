using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedidos.Model;

namespace RastreamentoPedidos.Data
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.ToTable("endereco").HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnType("integer").UseSerialColumn();
            builder.Property(x => x.Bairro)
                .HasColumnName("bairro")
                .HasColumnType("varchar")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.CEP)
                .HasColumnName("cep")
                .HasColumnType("varchar")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(x => x.Numero)
                .HasColumnName("numero")
                .HasColumnType("varchar");

            builder.Property(x => x.Cidade)
                .HasColumnName("cidade")
                .HasColumnType("varchar");

            builder.Property(x => x.Logradouro)
                .HasColumnName("lougradouro")
                .HasColumnType("varchar");

            builder.HasOne(e  => e.encomenda)
                   .WithMany(e => e.localizacao)
                   .HasForeignKey(e => e.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
