using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface ITpLogradouroRepository : IRepository<TpLogradouro>
    {
        Task<TpLogradouro> CarregarPorId(int id);
    }
}
