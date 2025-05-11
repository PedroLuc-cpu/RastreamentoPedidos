
using static RastreamentoPedidos.Model.ApplicationUser;

namespace RastreamentoPedido.Core.Model.Usuario
{
    public class UsuarioComRoles
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string DescricaoStatus { get; set; } = string.Empty;
        public StatusUsuario StatusUser { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
