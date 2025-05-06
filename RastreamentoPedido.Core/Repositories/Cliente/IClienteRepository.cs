using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<IList<Cliente>> CarregarTodos();
        Task<Cliente> CarregarPorId(long id);
        Task<Cliente> CarregarPorEmail(string email);
        Task<Cliente> CarregarPorDocumento(string documento);
        Task<Cliente> Adicionar(Cliente cliente);
    }
}
