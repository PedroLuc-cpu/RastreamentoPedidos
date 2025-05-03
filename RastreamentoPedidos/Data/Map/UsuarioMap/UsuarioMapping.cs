
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedidos.Model.Usuario;

namespace RastreamentoPedidos.Data.Map.UsuarioMap
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(x => x.idUsuario);
            builder.Property(x => x.idUsuario).UseSerialColumn().HasColumnName("idUsuario");
            builder.Property(x => x.nome).HasColumnName("nome");
            builder.Property(x => x.login).HasColumnName("login");
            builder.Property(x => x.senha).HasColumnName("senha");
            builder.Property(x => x.ativo).HasDefaultValueSql("true").HasColumnName("ativo");
            builder.Property(x => x.bio).HasColumnName("bio");
        }
    }
}