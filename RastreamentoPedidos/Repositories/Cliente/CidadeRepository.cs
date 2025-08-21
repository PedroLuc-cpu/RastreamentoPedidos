using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Endereco;
using RastreamentoPedido.Core.Repositories.Clientes;


namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class CidadeRepository : ICidadeRepository
    {
        private readonly IDapperContext _context;
        private readonly IUFRepository _ufRepository;
        public CidadeRepository(IUFRepository ufRepository, IDapperContext context)
        {
            _context = context;
            _ufRepository = ufRepository;
        }
        public async Task<Cidade> CarregarPorId(long id)
        {
            Cidade cidade = new Cidade();
            var sql = """SELECT * FROM cidade WHERE "idCidade" = @IdCidade """;
            var parametros = new { IdCidade = id }; 
            using var conexao = _context.ConnectionCreate();
            var retorno = await conexao.QueryFirstOrDefaultAsync(sql, parametros);
            if (retorno != null)
            {
                cidade.IdCidade = retorno.IdCidade;
                cidade.Nome = retorno.nome;
                cidade.CodIbge = retorno.codIBJE;
                cidade.UF = await _ufRepository.CarregarPorId(retorno.IdUF);
            }
            return cidade;
        }
    }
}
