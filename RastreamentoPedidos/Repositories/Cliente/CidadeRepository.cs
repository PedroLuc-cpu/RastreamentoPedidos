using Microsoft.EntityFrameworkCore;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedidos.Data;

namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly RastreamentoPedidosContext _context;
        private readonly IUFRepository _ufRepository;
        public CidadeRepository(IUFRepository ufRepository, RastreamentoPedidosContext context)
        {
            _context = context;
            _ufRepository = ufRepository;
        }
        public async Task<Cidade> CarregarPorId(long id)
        {
            Cidade cidade = new Cidade();
            var retorno = await _context.Cidades.FirstOrDefaultAsync(x => x.IdCidade == id);
            if (retorno != null)
            {
                cidade.IdCidade = retorno.IdCidade;
                cidade.Nome = retorno.Nome;
                cidade.UF = await _ufRepository.CarregarPorId(retorno.IdUF);
            }
            return cidade;
        }
    }
}
