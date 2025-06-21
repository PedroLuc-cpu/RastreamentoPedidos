using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedido.Core.Repositories.Encomenda
{
    public interface IStatusEncomendaRepository : IRepository<StatusEncomenda>
    {
     
        Task<StatusEncomenda> ObterStatusPorId(int id);
        Task<IList<StatusEncomenda>> ObterTodosStatus();
    }
}
