using RastreamentoPedidos.Model;
using RastreamentoPedidos.Model.DTO;
using RastreamentoPedidos.Model.Encomenda;
using RastreamentoPedidos.Repositories.Interface;

namespace RastreamentoPedidos.Repositories
{
    public class EncomendaRepository : IEncomendaRepository
    {
        public Task<Encomendas> AdicionarNovaEncomenda(EncomendaDTO encomenda)
        {
            throw new NotImplementedException();
        }

        public Task<Encomendas> AtualizizarStatusEncomenda(int id, StatusEntregaEnum statusCodigo)
        {
            throw new NotImplementedException();
        }
    }
}
