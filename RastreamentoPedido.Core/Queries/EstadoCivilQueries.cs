using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Queries
{
    public class EstadoCivilQueries
    {
        public static QueryParamsSQL ObterEstadoCivilPorId(int id)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT idestadocivil, \"EstadoCivilDescricao\" FROM \"estadoCivil\" WHERE idestadocivil = @idestadocivil",
                Parametros = new Dictionary<string, object> { { "idestadocivil", id } }
            };
        }
        public static QueryParamsSQL ObterTodosEstadosCivis()
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT idestadocivil, \"EstadoCivilDescricao\" FROM \"estadoCivil\"",
                Parametros = new Dictionary<string, object>()
            };
        }
        public static QueryParamsSQL ObterEstadoCivilPorDescricao(string estadoCivilDescricao)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT idestadocivil, \"EstadoCivilDescricao\" FROM \"estadoCivil\" WHERE \"EstadoCivilDescricao\" = @EstadoCivilDescricao",
                Parametros = new Dictionary<string, object> { { "EstadoCivilDescricao", estadoCivilDescricao } }
            };
        }
        public static QueryParamsSQL AdicionarEstadoCivil(EstadoCivil estadoCivil)
        {
            return new QueryParamsSQL
            {
                Sql = "INSERT INTO \"estadoCivil\" (\"EstadoCivilDescricao\") VALUES (@EstadoCivilDescricao) RETURNING idestadocivil",
                Parametros = new Dictionary<string, object>
                {
                    { "EstadoCivilDescricao", estadoCivil.EstadoCivilDescricao }
                }
            };
        }
        //public static QueryParamsSQL AtualizarEstadoCivil(EstadoCivil estadoCivil)
        //{
        //    return new QueryParamsSQL
        //    {
        //        Sql = "UPDATE estadoCivil SET \"estadoCivilDescricao\" = @estadoCivilDescricao WHERE \"idestadocivil\" = @idestadocivil;",
        //        Parametros = new Dictionary<string, object>
        //        {
        //            { "id", estadoCivil.Id },
        //            { "estadoCivilDescricao", estadoCivil.EstadoCivilDescricao }
        //        }
        //    };
        //}
    }
}
