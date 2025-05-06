using RastreamentoPedido.Core.Queries;

namespace RastreamentoPedido.Core.Data.Queries.Clientes
{
    public class UfQueries
    {
        public static QueryParamsSQL CarregarUf()
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT \"idUF\", sigla FROM uf",
                Parametros = new Dictionary<string, object>()
            };
        }

        public static QueryParamsSQL CarregarUfPorId(int id)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT \"idUF\", sigla FROM uf WHERE \"idUF\" = @id",
                Parametros = new Dictionary<string, object> { { "id", id } }
            };
        }
    }
}
