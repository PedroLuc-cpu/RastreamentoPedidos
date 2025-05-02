
namespace RastreamentoPedidos.Data.Queries
{
    public class QueryParamsSQL
    {
        public QueryParamsSQL(string sql, IDictionary<string, object> parametros)
        {
            Sql = sql;
            Parametros = parametros;
        }
        public QueryParamsSQL() { }

        public string Sql { get; set; } = string.Empty;
        public IDictionary<string, object> Parametros { get; set; } = new Dictionary<string, object>();
    }
}