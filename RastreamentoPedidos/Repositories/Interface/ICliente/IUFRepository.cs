using RastreamentoPedidos.Model.Clientes;

namespace RastreamentoPedidos.Repositories.Interface.ICliente
{
    public interface IUFRepository : IRepository<UF>
    {
        Task<UF> CarregarPorId(long id);
    }
}
