using Microsoft.EntityFrameworkCore;
using RastreamentoPedidos.Data;
using RastreamentoPedidos.Model.Clientes;
using RastreamentoPedidos.Repositories.Interface.ICliente;

namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class UFRepository : IUFRepository
    {
        private readonly RastreamentoPedidosContext _context;

        public UFRepository(RastreamentoPedidosContext context)
        {
            _context = context;            
        }
        public async Task<UF> CarregarPorId(long id)
        {
            UF uF = new UF();
            var retorno = await _context.uFs.FirstOrDefaultAsync(x => x.ifUF == id);

            if (retorno != null)
            {
                uF.ifUF = retorno.ifUF;
                uF.sigla = retorno.sigla;
            }
            return retorno;
            
        }
    }
}
