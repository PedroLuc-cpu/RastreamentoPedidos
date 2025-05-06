using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<IList<Endereco>> CarregarPorIdCliente(int idCliente);
    }
}
