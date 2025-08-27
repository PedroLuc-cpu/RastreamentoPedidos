using RastreamentoPedido.Core.Model.Endereco;

namespace RastreamentoPedido.Core.Repositories.Cliente
{
    public interface IPaisRepository
    {
        Task<Pais> CarregarPorId(int id);
        Task<IList<Pais>> CarregarTodos();
    }
}
