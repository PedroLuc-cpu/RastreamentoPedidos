using Dapper;
using RastreamentoPedido.Core.Model.Clientes;
using RastreamentoPedido.Core.Queries.Clientes;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedidos.Data;

namespace RastreamentoPedidos.Repositories.ClienteRepository
{
    public class TelefoneRepository : ITelefoneRepository
    {
        private readonly DapperContext _dapper;
        
        public TelefoneRepository(DapperContext dapper)
        {
            _dapper = dapper;
        }

        public async Task<Telefone> AdicionarTelefone(Telefone telefone)
        {
            using (var connection = _dapper.ConnectionCreate())
            {
                var paramSQL = TelefoneQueries.AdicionarTelefone(telefone);
                var id = await connection.ExecuteScalarAsync<int>(paramSQL.Sql, paramSQL.Parametros);
                telefone.IdTelefoneCliente = id;
            }
            return telefone;
        }

        public async Task<Telefone> AtualizarTelefone(Telefone telefone)
        {
            var sql = "UPDATE telefone SET prefixo = @prefixo, numero = @numero, padrao = @padrao WHERE \"IdCliente\" = @IdCliente";
            var parametros = new Dictionary<string, object>
            {
                    { "prefixo", telefone.Prefixo },
                    { "numero", telefone.Numero },
                    { "padrao", telefone.Padrao },
                    { "IdCliente", telefone.IdCliente }
            };

            using (var connection = _dapper.ConnectionCreate())
            {
                var linhasAfetadas = await connection.ExecuteAsync(sql, parametros);
                return telefone;
            }
        }

        public async Task<IList<Telefone>> CarregarPorIdCliente(int idCliente)
        {
            IList<Telefone> telefones = [];
            using (var connection = _dapper.ConnectionCreate())
            {
                var paramSQL = TelefoneQueries.ObterTelefonePorIdCliente(idCliente);
                var registros = await connection.QueryFirstOrDefaultAsync<Telefone>(paramSQL.Sql, paramSQL.Parametros);
                if (registros != null)
                {
                    telefones.Add(registros);
                }

            }
            return telefones;

        }

        public async Task<Telefone> ObterTelefonePorIdCliente(int idCliente)
        {
            Telefone telefone = new();
            using (var connection = _dapper.ConnectionCreate())
            {
                var paramSQL = TelefoneQueries.ObterTelefonePadraoPorIdCliente(idCliente);
                var registro = await connection.QueryFirstOrDefaultAsync(paramSQL.Sql, paramSQL.Parametros);
                if (registro != null)
                {
                    telefone.IdTelefoneCliente = registro.IdTelefoneCliente;
                    telefone.Prefixo = registro.prefixo;
                    telefone.Numero = registro.numero;
                    telefone.IdCliente = registro.IdCliente;
                    telefone.Padrao = registro.padrao;
                }
                return telefone;
            }
        }

        public async Task<Telefone> ObterTelefonePorIdTelefone(int idTelefoneCliente)
        {
            Telefone telefone = new Telefone();
            using (var connection = _dapper.ConnectionCreate())
            {
                var paramSQL = TelefoneQueries.ObterTelefonePorIdTelefone(idTelefoneCliente);
                var registro = await connection.QueryFirstOrDefaultAsync(paramSQL.Sql, paramSQL.Parametros);
                if (registro != null)
                {
                    telefone.IdTelefoneCliente = registro.idTelefoneCliente;
                    telefone.Prefixo = registro.prefixo;
                    telefone.Numero = registro.numero;
                    telefone.IdCliente = registro.IdCliente;
                    telefone.Padrao = registro.padrao;
                }
                return telefone;
            }
        }
    }
}
