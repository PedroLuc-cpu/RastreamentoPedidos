using RastreamentoPedidos.Model.Clientes;

namespace RastreamentoPedidos.Repositories.Interface.ICliente
{
    public interface ITpLogradouroRepository : IRepository<TpLogradouro>
    {
        Task<TpLogradouro> CarregarPorId(long id);
    }
}
