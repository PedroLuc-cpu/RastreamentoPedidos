using RastreamentoPedidos.Model.Clientes;

namespace RastreamentoPedidos.Repositories.Interface.ICliente
{
    public interface IEnderecoRepository : IRepository<Endereco>
    {
        Task<IList<Endereco>> CarregarPorIdCliente(int idCliente);
    }
}
