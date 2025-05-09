using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RastreamentoPedido.Core.Model.Usuario;

namespace RastreamentoPedidos.Data.Map.Usuarios
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("usuario");
            builder.HasKey(x => x.idUsuario);
            builder.Property(x => x.idUsuario).HasColumnName("idUsuario");
            builder.Property(x => x.nomeUsuario).HasColumnName("nome");
            builder.Property(x => x.email).HasColumnName("email");
            builder.Property(x => x.senha).HasColumnName("senha");
            builder.Property(x => x.senhaConfirmacao).HasColumnName("senhaConfirmacao");
            builder.Property(x => x.funcao).HasColumnName("funcao");
            builder.Property(x => x.bio).HasColumnName("bio");
        }
    }
}