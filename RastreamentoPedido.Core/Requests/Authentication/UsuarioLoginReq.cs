using System.ComponentModel.DataAnnotations;

namespace RastreamentoPedido.Core.Requests.Authentication
{
    public class UsuarioLoginReq
    {
        [Required(ErrorMessage = "O nome de usuário é obrgatório.")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; set; } = string.Empty;
    }
}
