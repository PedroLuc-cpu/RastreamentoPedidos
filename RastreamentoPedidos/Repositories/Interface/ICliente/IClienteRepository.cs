using RastreamentoPedidos.Model.Clientes;

namespace RastreamentoPedidos.Repositories.Interface.ICliente
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
