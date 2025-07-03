using RastreamentoPedido.Core.Model.Endereco;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface IEnderecoRepository : IRepository<Enderecos>
    {
        Task<IList<Enderecos>> CarregarPorIdCliente(int idCliente);
    }
}
