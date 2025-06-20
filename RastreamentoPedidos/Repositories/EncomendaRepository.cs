using RastreamentoPedido.Core.Model.Encomenda;
using RastreamentoPedido.Core.Repositories;

namespace RastreamentoPedidos.Repositories
{
    public class EncomendaRepository : IEncomendaRepository
    {
        public Task<Encomendas> AdicionarNovaEncomenda(Encomendas encomenda)
        {
            throw new NotImplementedException();
        }

        public Task<Encomendas> AtualizizarStatusEncomenda(int id, StatusEntregaEnum statusCodigo)
        {
            throw new NotImplementedException();
        }

        public Task<Encomendas> CarregarEncomendaPorDocumento(int documento)
        {
            throw new NotImplementedException();
        }

        public Task<Encomendas> CarregarEncomendaPorId(int idCliente)
        {
            throw new NotImplementedException();
        }
    }
}
