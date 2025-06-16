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
        public async Task<TpLogradouro> CarregarPorId(int id)
        {
            TpLogradouro tpLogradouro = new TpLogradouro();
            var retorno = await _context.TpLogradouros.FirstOrDefaultAsync(x => x.IdTpLogradouro == id);
            if (retorno != null)
            {
                tpLogradouro.IdTpLogradouro = retorno.IdTpLogradouro;
                tpLogradouro.Sigla = retorno.Sigla;
                tpLogradouro.Nome = retorno.Nome;
            }
            return tpLogradouro;
        }
    }
}
