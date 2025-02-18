using RastreamentoPedidos.Model.Clientes;

namespace RastreamentoPedidos.Repositories.Interface.ICliente
{
    public interface ICidadeRepository : IRepository<Cidade>
    {
        Task<Cidade> CarregarPorId(int id);
    }
}
