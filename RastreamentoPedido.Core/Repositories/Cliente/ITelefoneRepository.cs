using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface ITelefoneRepository : IRepository<Telefone>
    {
        Task<IList<Telefone>> CarregarPorIdCliente(int idCliente);
    }
}
