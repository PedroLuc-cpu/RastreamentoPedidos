using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Queries.Clientes
{
    public class TelefoneQueries
    {
        public static QueryParamsSQL AdicionarTelefone(Telefone telefone)
        {
            return new QueryParamsSQL
            {
                Sql = """INSERT INTO telefone ("idCliente", prefixo, numero, padrao) VALUES (@IdCliente, @Prefixo, @Numero, @Padrao);""",
                Parametros = new Dictionary<string, object>
                {
                    { "IdCliente", telefone.IdCliente },
                    { "Prefixo", telefone.Prefixo },
                    { "Numero", telefone.Numero },
                    { "Padrao", telefone.Padrao }
                }
            };
        }
        public static QueryParamsSQL ObterTelefonePorIdCliente(int idCliente)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM telefone WHERE \"idCliente\" = @IdCliente",
                Parametros = new Dictionary<string, object> { { "IdCliente", idCliente } }
            };
        }
        public static QueryParamsSQL ObterTelefonePorIdTelefone(int idTelefoneCliente)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM telefone WHERE \"idTelefone\" = @idTelefoneCliente",
                Parametros = new Dictionary<string, object> { { "idTelefoneCliente", idTelefoneCliente } }
            };
        }

        public static QueryParamsSQL ObterTelefonePadraoPorIdCliente(int idCliente)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM telefone WHERE \"idCliente\" = @IdCliente AND padrao = true",
                Parametros = new Dictionary<string, object> { { "IdCliente", idCliente } }
            };
        }

        public static QueryParamsSQL AtualizarTelefone(Telefone telefone)
        {
            return new QueryParamsSQL
            {
                Sql = "UPDATE telefone SET prefixo = @prefixo, numero = @numero, padrao = @padrao WHERE \"idTelefone\" = @IdTelefoneCliente",
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
