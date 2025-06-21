using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Encomenda;
using RastreamentoPedido.Core.Queries.Encomendas;
using RastreamentoPedido.Core.Repositories.Encomenda;

namespace RastreamentoPedidos.API.Repositories.Encomendas
{
    public class StatusEncomendaRepository : IStatusEncomendaRepository
    {
        private readonly IDapperContext _dapperContext;

        public StatusEncomendaRepository(IDapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<StatusEncomenda> ObterStatusPorId(int id)
        {
            StatusEncomenda statusEncomenda = new StatusEncomenda();
            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = StatusEncomendasQueries.CarregarStutusEncomendasPorId(id);
                var registro = await connection.QueryFirstOrDefaultAsync(paramSQL.Sql, paramSQL.Parametros);
                if (registro != null)
                {
                    if (registro.idestadocivil > 0)
                    {
                        statusEncomenda.Id = registro.id;
                        statusEncomenda.Status = registro.status;
                    }
                }
            }
            return statusEncomenda;
        }

        public async Task<IList<StatusEncomenda>> ObterTodosStatus()
        {
            IList<StatusEncomenda> statusEncomendas = new List<StatusEncomenda>();
            using (var connecion = _dapperContext.ConnectionCreate())
            {
                var paramSQL = StatusEncomendasQueries.CarregarTodosStatusEncomendas();
                var registros = await connecion.QueryAsync(paramSQL.Sql, paramSQL.Parametros);
                if (registros != null)
                {
                    foreach (var registro in registros)
                    {
                        if (registro.id > 0)
                        {
                            statusEncomendas.Add(new StatusEncomenda
                            {
                                Id = registro.id,
                                Status = registro.status
                            });
                        }
                    }
                }
                return statusEncomendas;
            }
        }
    }
}
