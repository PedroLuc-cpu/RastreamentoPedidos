using Dapper;
using RastreamentoPedidos.Data.Interface;
using RastreamentoPedidos.Data.Queries.Clientes;
using RastreamentoPedidos.Model.Clientes;
using RastreamentoPedidos.Repositories.Interface.ICliente;

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
        public async Task<Cidade> CarregarPorId(int id)
        {
            Cidade cidade = new Cidade();
            using (var connection = _dapper.ConnectionCreate())
            {
                var paramSQl = CidadeQueries.obterCidadePorId(id);
                var retorno = await connection.QuerySingleOrDefaultAsync(paramSQl.Sql, paramSQl.Parametros);
                if (retorno != null)
                {
                    cidade.idCidade = retorno.idCidade;
                    cidade.nome = retorno.nome;
                    cidade.idUF = retorno.idUF;
                    cidade.UF = await _ufRepository.CarregarPorId(retorno.idUF);
                }
                return cidade;

            }
        }
    }
}