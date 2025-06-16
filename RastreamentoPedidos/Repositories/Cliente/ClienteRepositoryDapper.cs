using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedidos.Data.Queries.Clientes;


namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class ClienteRepositoryDapper : ICidadeRepository
    {
        private readonly IDapperContext _dapper;
        private readonly IUFRepository _ufRepository;

        public ClienteRepositoryDapper(IDapperContext dapper, IUFRepository ufRepository)
        {
            _dapper = dapper;
            _ufRepository = ufRepository;
        }
        public async Task<Cidade> CarregarPorId(long id)
        {
            Cidade cidade = new Cidade();
            using (var connection = _dapper.ConnectionCreate())
            {
                var paramSQl = CidadeQueries.obterCidadePorId(id);
                var retorno = await connection.QuerySingleOrDefaultAsync(paramSQl.Sql, paramSQl.Parametros);
                if (retorno != null)
                {
                    cidade.IdCidade = retorno.idCidade;
                    cidade.Nome = retorno.nome;
                    cidade.IdUF = retorno.idUF;
                    cidade.UF = await _ufRepository.CarregarPorId(retorno.idUF);
                }
                return cidade;

            }
        }
    }
}