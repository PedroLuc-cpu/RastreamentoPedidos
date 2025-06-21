namespace RastreamentoPedido.Core.Queries.Encomendas
{
    public class StatusEncomendasQueries
    {
        public static QueryParamsSQL CarregarStutusEncomendasPorId(int id)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM status_entregas WHERE id = @id",
                Parametros = new Dictionary<string, object> { { "id", id } }
            };
        }

        public static QueryParamsSQL CarregarTodosStatusEncomendas()
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM status_entregas",
                Parametros = new Dictionary<string, object>()
            };
        }
    }
}
