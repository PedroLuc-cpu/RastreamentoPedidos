using RastreamentoPedido.Core.Model.Endereco;

namespace RastreamentoPedido.Core.Repositories.Clientes
{
    public interface IUFRepository : IRepository<UF>
    {
        Task<UF> CarregarPorId(int id);
        Task<List<UF>> CarregarTodasUf();
    }
}
