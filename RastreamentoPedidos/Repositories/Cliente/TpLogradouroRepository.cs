using Microsoft.EntityFrameworkCore;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedidos.Data;


namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class TpLogradouroRepository : ITpLogradouroRepository
    {
        private readonly RastreamentoPedidosContext _context;
        public TpLogradouroRepository(RastreamentoPedidosContext context)
        {
            _context = context;
        }
        public async Task<TpLogradouro> CarregarPorId(long id)
        {
            TpLogradouro tpLogradouro = new TpLogradouro();
            var retorno = await _context.tpLogradouros.FirstOrDefaultAsync(x => x.idTpLogradouro == id);
            if (retorno != null)
            {
                tpLogradouro.idTpLogradouro = retorno.idTpLogradouro;
                tpLogradouro.sigla = retorno.sigla;
                tpLogradouro.nome = retorno.nome;
            }
            return tpLogradouro;
        }
    }
}
