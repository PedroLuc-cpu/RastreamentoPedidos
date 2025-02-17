using RastreamentoPedidos.Model.DTO;

namespace RastreamentoPedidos.Repositories.Interface
{
    public interface IClienteRepository : IRepository<ClienteDto>
    {
        Task<IEnumerable<ClienteDto>> CarregarTodos();
        Task<ClienteDto> CarregarPorId(long id);
        Task<ClienteDto> CarregarPorEmail(string email);
        Task<ClienteDto> AdicionarClientes(ClienteDto cliente);
    }
}
