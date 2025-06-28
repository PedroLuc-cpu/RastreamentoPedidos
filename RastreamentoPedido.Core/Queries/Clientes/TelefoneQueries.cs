using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Queries.Clientes
{
    public class TelefoneQueries
    {
        public static QueryParamsSQL AdicionarTelefone(Telefone telefone)
        {
            return new QueryParamsSQL
            {
                Sql = "INSERT INTO telefone (prefixo, numero, \"IdCliente\", padrao) VALUES (@prefixo, @numero, @IdCliente, @padrao) RETURNING \"idTelefoneCliente\"",
                Parametros = new Dictionary<string, object>
                {
                    { "prefixo", telefone.Prefixo },
                    { "numero", telefone.Numero },
                    { "IdCliente", telefone.IdCliente },
                    { "Padrao", telefone.Padrao }
                }
            };
        }
        public static QueryParamsSQL ObterTelefonePorIdCliente(int idCliente)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM telefone WHERE \"IdCliente\" = @IdCliente",
                Parametros = new Dictionary<string, object> { { "IdCliente", idCliente } }
            };
        }
        public static QueryParamsSQL ObterTelefonePorIdTelefone(int idTelefoneCliente)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM telefone WHERE \"idTelefoneCliente\" = @idTelefoneCliente",
                Parametros = new Dictionary<string, object> { { "idTelefoneCliente", idTelefoneCliente } }
            };
        }

        public static QueryParamsSQL ObterTelefonePadraoPorIdCliente(int idCliente)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM telefone WHERE \"IdCliente\" = @IdCliente AND \"padrao\" = true",
                Parametros = new Dictionary<string, object> { { "IdCliente", idCliente } }
            };
        }

        public static QueryParamsSQL AtualizarTelefone(Telefone telefone)
        {
            return new QueryParamsSQL
            {
                Sql = "UPDATE telefone SET prefixo = @prefixo, numero = @numero, padrao = @padrao WHERE \"idTelefoneCliente\" = @IdTelefoneCliente",
                Parametros = new Dictionary<string, object>
                {
                    { "Prefixo", telefone.Prefixo },
                    { "Numero", telefone.Numero },
                    { "Padrao", telefone.Padrao },
                    { "IdTelefoneCliente", telefone.IdTelefoneCliente }
                }
            };
        }

    }
}
