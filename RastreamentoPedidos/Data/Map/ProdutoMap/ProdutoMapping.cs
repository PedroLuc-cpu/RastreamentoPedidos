using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.ProdutoModel;

namespace RastreamentoPedidos.API.Data.Map.ProdutoMap
{
    public class ProdutoMapping : IEntityTypeConfiguration<ProdutoModel>
    {
        public void Configure(EntityTypeBuilder<ProdutoModel> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasColumnType("varchar(150)").IsRequired();
            builder.Property(p => p.Observacao).HasColumnType("varchar(500)").IsRequired(false);
            builder.Property(p => p.CodigoBarras).HasColumnType("varchar(50)");
            builder.Property(p => p.Codigo).HasColumnType("varchar(50)").IsRequired();
            builder.Property(p => p.UnidadeMedida).HasColumnType("varchar(20)").IsRequired();
            builder.Property(p => p.PrecoVenda).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(p => p.PrecoCusto).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Ativo).HasColumnType("boolean").HasDefaultValueSql("true");
            builder.Property(p => p.DataCadastro).HasColumnType("TIMESTAMP").HasDefaultValueSql("NOW()");
            builder.Property(p => p.IdCategoria).HasColumnType("int").IsRequired(false);
            builder.Property(p => p.IdMarca).HasColumnType("int").IsRequired(false);
            builder.Property(p => p.ImagemUrl).HasColumnType("text").IsRequired(false);
            builder.HasOne(p => p.ProdutoCategoria)
                   .WithMany()
                   .HasForeignKey(p => p.IdCategoria)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);
            builder.HasOne(p => p.ProdutoMarca)
                   .WithMany()
                   .HasForeignKey(p => p.IdMarca)
                   .OnDelete(DeleteBehavior.Restrict)
                   .IsRequired(false);
            builder.HasOne(p => p.ProdutoPeso)
                   .WithOne(pp => pp.Produto)
                   .HasForeignKey<ProdutoPeso>(pp => pp.IdProduto)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);
            builder.HasOne(p => p.ProdutoEncargos)
                   .WithOne(pe => pe.Produto)
                   .HasForeignKey<ProdutoEncargos>(pe => pe.IdProduto)
                   .OnDelete(DeleteBehavior.Cascade)
                   .IsRequired(false);


        }
    }
}
