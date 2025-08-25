using Dapper;
using RastreamentoPedido.Core.Data;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Queries;
using RastreamentoPedido.Core.Repositories.IEstadoCivilRepository;

namespace RastreamentoPedidos.API.Repositories.Cliente
{
    public class EstadoCivilRepository(IDapperContext dapperContext) : IEstadoCivilRepository
    {
        private readonly IDapperContext _dapperContext = dapperContext;

        public async Task<EstadoCivil> AdicionarEstadoCivil(EstadoCivil estadoCivil)
        {
            using var connection = _dapperContext.ConnectionCreate();
            var paramSQL = EstadoCivilQueries.AdicionarEstadoCivil(estadoCivil);
            var id = await connection.ExecuteScalarAsync<int>(paramSQL.Sql, paramSQL.Parametros);
            if (id > 0)
            {
                estadoCivil.Id = id;
            }
            return estadoCivil;

        }
        public async Task<EstadoCivil> AtualizarEstadoCivil(EstadoCivil estadoCivil)
        {
            var sql = "UPDATE est_civil SET \"estCivil\" = @EstadoCivilDescricao WHERE \"idEstCivil\" = @idestadocivil;";
            var parametros = new Dictionary<string, object>
            {
                { "idestadocivil", estadoCivil.Id },
                { "EstadoCivilDescricao", estadoCivil.EstadoCivilDescricao }
            };
            using var connection = _dapperContext.ConnectionCreate();
            var linhasAfetadas = await connection.ExecuteAsync(sql, parametros);
            return estadoCivil;
        }

        public async Task<EstadoCivil> CarregarEstadoCivilPorDescricao(string descricao)
        {
            EstadoCivil estadoCivil = new();
            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = EstadoCivilQueries.ObterEstadoCivilPorDescricao(descricao);
                var registro = await connection.QueryFirstOrDefaultAsync(paramSQL.Sql, paramSQL.Parametros);
                if (registro != null)
                {
                    if (registro.idEstCivil > 0)
                    {
                        estadoCivil.Id = registro.idEstCivil;
                        estadoCivil.EstadoCivilDescricao = registro.estCivil;
                    }
                }

            }
            return estadoCivil;
        }

        public async Task<EstadoCivil> CarregarEstadoCivilPorId(int id)
        {
            EstadoCivil estadoCivil = new();
            using (var connection = _dapperContext.ConnectionCreate())
            {
                var paramSQL = EstadoCivilQueries.ObterEstadoCivilPorId(id);
                var registro = await connection.QueryFirstOrDefaultAsync(paramSQL.Sql, paramSQL.Parametros);
                if (registro != null)
                {
                    if (registro.idEstCivil > 0)
                    {
                        estadoCivil.Id = registro.idEstCivil;
                        estadoCivil.EstadoCivilDescricao = registro.estCivil;
                    }
                }

            }
            return estadoCivil;
        }

        public async Task<IEnumerable<EstadoCivil>> CarregarTodosEstadosCivis()
        {
            IList<EstadoCivil> estadoCivils = [];

            using var connection = _dapperContext.ConnectionCreate();
            var paramSQL = EstadoCivilQueries.ObterTodosEstadosCivis();
            var registros = await connection.QueryAsync(paramSQL.Sql, paramSQL.Parametros);
            if (registros != null)
            {
                foreach (var registro in registros)
                {
                    estadoCivils.Add(new EstadoCivil
                    {
                        Id = registro.idEstCivil,
                        EstadoCivilDescricao = registro.estCivil
                    });
                }
            }
            return estadoCivils;
        }
        public Task<bool> DeletarEstadoCivil(int Id)
        {
            var sql = "DELETE FROM est_civil WHERE \"idEstCivil\" = @idestadocivil;";
            var parametros = new Dictionary<string, object>
            {
                { "idestadocivil", Id },
            };
            using var connection = _dapperContext.ConnectionCreate();
            var linhasAfetadas = connection.ExecuteAsync(sql, parametros);
            return Task.FromResult(linhasAfetadas.Result > 0);

        }
    }
}
