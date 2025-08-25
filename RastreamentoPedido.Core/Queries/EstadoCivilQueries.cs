using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Queries
{
    public class EstadoCivilQueries
    {
        public static QueryParamsSQL ObterEstadoCivilPorId(int id)
        {
            return new QueryParamsSQL
            {
                Sql = """SELECT * FROM est_civil WHERE "idEstCivil" = @idestadocivil;""",
                Parametros = new Dictionary<string, object> { { "idestadocivil", id } }
            };
        }
        public static QueryParamsSQL ObterTodosEstadosCivis()
        {
            return new QueryParamsSQL
            {
                Sql = """SELECT * FROM est_civil """,
                Parametros = new Dictionary<string, object>()
            };
        }
        public static QueryParamsSQL ObterEstadoCivilPorDescricao(string estadoCivilDescricao)
        {
            return new QueryParamsSQL
            {
                Sql = """SELECT * FROM est_civil WHERE "estCivil" = @EstadoCivilDescricao""",
                Parametros = new Dictionary<string, object> { { "EstadoCivilDescricao", estadoCivilDescricao } }
            };
        }
        public static QueryParamsSQL AdicionarEstadoCivil(EstadoCivil estadoCivil)
        {
            return new QueryParamsSQL
            {
                Sql = "INSERT INTO est_civil (\"estCivil\") VALUES (@EstadoCivilDescricao) RETURNING \"idEstCivil\"",
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
