using RastreamentoPedidos.Model;
using RastreamentoPedidos.Model.DTO;
using RastreamentoPedidos.Model.Encomenda;

namespace RastreamentoPedidos.Repositories.Interface
{
    public interface IEncomendaRepository : IRepository<Encomendas>
    {
        Task<Encomendas> AdicionarNovaEncomenda(EncomendaDTO encomenda);
        Task<Encomendas> AtualizizarStatusEncomenda(int id, StatusEntregaEnum statusCodigo);
        Task<Encomendas> CarregarEncomendaPorId(int idCliente);
        Task<Encomendas> CarregarEncomendaPorDocumento(int documento);
    }
}
