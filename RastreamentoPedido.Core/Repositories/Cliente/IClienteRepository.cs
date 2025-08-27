using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Repositories.Cliente
{
    public interface IClienteRepository : IRepository<ClienteModel>
    {
        Task<IList<ClienteModel>> CarregarTodos();
        Task<ClienteModel> CarregarPorId(int id);
        Task<ClienteModel> CarregarPorEmail(string email);
        Task<ClienteModel> CarregarPorDocumento(string documento);
        Task<ClienteModel> Inserir(ClienteModel cliente);
        Task<ClienteModel> Alterar(ClienteModel cliente);
    }
}
