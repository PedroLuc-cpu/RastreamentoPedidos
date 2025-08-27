using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Data.Queries.Clientes;
using RastreamentoPedido.Core.Model.Endereco;
using RastreamentoPedido.Core.Repositories.Cliente;
using RastreamentoPedido.Core.Repositories.Clientes;

namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class UFRepository(IDapperContext dapper, IPaisRepository paisRepository) : IUFRepository
    {
        private readonly IDapperContext _dapper = dapper;
        private readonly IPaisRepository _paisRepository = paisRepository;

        public async Task<UF> CarregarPorId(int id)
        {
            UF uf = new();
            using var connection = _dapper.ConnectionCreate();
            var paramSQl = UfQueries.CarregarUfPorId(id);
            var retorno = await connection.QuerySingleOrDefaultAsync(paramSQl.Sql, paramSQl.Parametros);
            if (retorno != null)
            {
                await PreencherObj(retorno);
            }
            return uf;

        }

        public async Task<IList<UF>> CarregarTodasUf()
        {
            IList<UF> ufs = [];
            using var connection = _dapper.ConnectionCreate();
            var paramSQl = UfQueries.CarregarUf();
            var retorno = await connection.QueryAsync(paramSQl.Sql, paramSQl.Parametros);
            if (retorno != null)
            {
                foreach (var item in retorno)
                {
                    ufs.Add(await PreencherObj(item));
                }
            }
            return ufs;
        }
        private async Task<UF> PreencherObj(dynamic retorno)
        {
            UF uf = new();
            try
            {
                uf.IdUF = retorno.idUF;
                uf.Sigla = retorno.sigla;
                uf.IdPais = retorno.idPais;
                uf.Pais = await _paisRepository.CarregarPorId(retorno.idPais);
                return uf;
            }
            catch(Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao preencher idUF: {uf.IdUF} - ERRO: {ex.Message}");
            }
        } 
    }
}
