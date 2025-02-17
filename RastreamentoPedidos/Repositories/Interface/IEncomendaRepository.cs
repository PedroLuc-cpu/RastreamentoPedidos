using RastreamentoPedidos.Model;
using RastreamentoPedidos.Model.DTO;

namespace RastreamentoPedidos.Repositories.Interface
{
    public interface IEncomendaRepository : IRepository<Encomenda>
    {
        Task<Encomenda> AdicionarNovaEncomenda(EncomendaDTO encomenda);
        Task<Encomenda> AtualizizarStatusEncomenda(int id, StatusEntregaEnum statusCodigo);
    }
}
