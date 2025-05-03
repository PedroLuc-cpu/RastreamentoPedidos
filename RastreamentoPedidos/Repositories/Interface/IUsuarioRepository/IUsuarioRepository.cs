using RastreamentoPedidos.Model.Usuario;

namespace RastreamentoPedidos.Repositories.Interface.IUsuarioRepository
{
    public interface IUsuarioRepository
    {
        Task<Usuario> CarregarPorId(int id);
    }
}