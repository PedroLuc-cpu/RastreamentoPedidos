using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;


namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class TpLogradouroRepository : ITpLogradouroRepository
    {
        private readonly IDapperContext _context;
        public TpLogradouroRepository(IDapperContext context)
        {
            _context = context;
        }
        public async Task<TpLogradouro> CarregarPorId(int id)
        {
            TpLogradouro tpLogradouro = new TpLogradouro();
            var sql = """SELECT * FROM tpLogradouros WHERE "idTpLogradouro" = @IdTpLogradouro """;
            var parametros = new { IdTpLogradouro = id };
            using var conexao = _context.ConnectionCreate();

            var retorno = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (retorno != null)
            {
                tpLogradouro.IdTpLogradouro = retorno.idLogradouro;
                tpLogradouro.Sigla = retorno.sigla;
                tpLogradouro.Nome = retorno.nome;
            }
            return tpLogradouro;
        }
    }
}
