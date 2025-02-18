using Microsoft.EntityFrameworkCore;
using RastreamentoPedidos.Data;
using RastreamentoPedidos.Model.Clientes;
using RastreamentoPedidos.Repositories.Interface.ICliente;

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
        public async Task<Cidade> CarregarPorId(int id)
        {
            Cidade cidade = new Cidade();
            var retorno = await _context.cidades.FirstOrDefaultAsync(x => x.idCidade == id);
            if (retorno != null)
            {
                cidade.idCidade = retorno.idCidade;
                cidade.nome = retorno.nome;
                cidade.UF = await _ufRepository.CarregarPorId(retorno.idUF);
            }
            return cidade;
        }
    }
}
