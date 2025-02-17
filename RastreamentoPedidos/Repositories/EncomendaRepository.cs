using RastreamentoPedidos.Model;
using RastreamentoPedidos.Model.DTO;
using RastreamentoPedidos.Repositories.Interface;

namespace RastreamentoPedidos.Repositories
{
    public class EncomendaRepository : IEncomendaRepository
    {
        public Task<Encomenda> AdicionarNovaEncomenda(EncomendaDTO encomenda)
        {
            throw new NotImplementedException();
        }

        public Task<Encomenda> AtualizizarStatusEncomenda(int id, StatusEntregaEnum statusCodigo)
        {
            throw new NotImplementedException();
        }
    }
}
