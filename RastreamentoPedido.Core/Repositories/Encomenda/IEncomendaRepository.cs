using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedido.Core.Repositories.Encomenda
{
    public interface IEncomendaRepository : IRepository<Encomendas>
    {
        Task<Encomendas> AdicionarNovaEncomenda(Encomendas encomenda);
        Task<Encomendas> AtualizizarStatusEncomenda(int id, StatusEntregaEnum statusCodigo);
        Task<Encomendas> CarregarEncomendaPorId(int idCliente);
        Task<Encomendas> CarregarEncomendaPorDocumento(int documento);
    }
}
