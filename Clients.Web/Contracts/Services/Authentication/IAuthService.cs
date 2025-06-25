using RastreamentoPedido.Core.Model.Usuario;
using RastreamentoPedido.Core.Requests.Authentication;

namespace Clients.Web.Contracts.Services.Authentication
{
    public interface IAuthService
    {
        Task<UsuarioRespostaLogin> Login(UsuarioLoginReq loginReq);
        Task Logout();
    }
}
