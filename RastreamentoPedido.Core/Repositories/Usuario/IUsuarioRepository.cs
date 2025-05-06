using RastreamentoPedido.Core.Model.Usuario;

namespace RastreamentoPedido.Core.Repositories.Interface.IUsuarioRepository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> CarregarPorId(int id);
    }
}