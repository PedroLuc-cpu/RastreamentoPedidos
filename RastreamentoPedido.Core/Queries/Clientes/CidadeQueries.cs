using RastreamentoPedido.Core.Queries;

namespace RastreamentoPedidos.Data.Queries.Clientes
{
    public class CidadeQueries
    {
        public static QueryParamsSQL obterCidadePorId(long id)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT \"idCidade\", nome, \"idUF\" FROM cidade WHERE \"idCidade\" = @id",
                Parametros = new Dictionary<string, object> { { "id", id } }
            };
        }
    }
}