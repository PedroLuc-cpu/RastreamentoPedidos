using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Data.Queries.Clientes;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;

namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class UFRepository : IUFRepository
    {
        private readonly IDapperContext _dapper;

        public UFRepository(IDapperContext dapper)
        {
            _dapper = dapper;
        }
        public async Task<UF> CarregarPorId(int id)
        {
            UF uf = new UF();
            using (var connection = _dapper.ConnectionCreate())
            {
                var paramSQl = UfQueries.CarregarUfPorId(id);
                var retorno = await connection.QuerySingleOrDefaultAsync(paramSQl.Sql, paramSQl.Parametros);
                if (retorno != null)
                {
                   uf.IdUF = retorno.idUF;
                   uf.Sigla = retorno.sigla;
                }
                return uf;
            }

        }

        public async Task<List<UF>> CarregarTodasUf()
        {
            IList<UF> ufs = new List<UF>();
            using (var connection = _dapper.ConnectionCreate())
            {
                var paramSQl = UfQueries.CarregarUf();
                var retorno = await connection.QueryAsync(paramSQl.Sql, paramSQl.Parametros);
                if (retorno != null)
                {
                    foreach (var item in retorno)
                    {
                        ufs.Add(new UF
                        {
                            IdUF = item.idUF,
                            Sigla = item.sigla
                        });
                    }
                }
                return ufs.ToList();
            }
        }
    }
}
