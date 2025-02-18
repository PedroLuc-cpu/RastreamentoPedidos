using RastreamentoPedidos.Model.Clientes;

namespace RastreamentoPedidos.Repositories.Interface.ICliente
{
    public interface ITelefoneRepository : IRepository<Telefone>
    {
        Task<IList<Telefone>> CarregarPorIdCliente(int idCliente);
    }
}
