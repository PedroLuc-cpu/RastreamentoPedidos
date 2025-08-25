using RastreamentoPedido.Core.Model.Clientes;

namespace RastreamentoPedido.Core.Queries.Clientes
{
    public class ClienteQueries
    {
        public static QueryParamsSQL AdicionarCliente(Cliente cliente)
        {
            return new QueryParamsSQL
            {
                Sql = "INSERT INTO \"cliente\" (\"email\", \"nome\", \"documento\", \"ativo\", \"sexo\", \"idEstadoCivil\", \"dataNascimento\" )" +
                      "VALUES (@Email, @Nome, @Documento, @Ativo, @Sexo, @EstadoCivilId, @DataNascimento) RETURNING *",
                Parametros = new Dictionary<string, object>
                {
                    { "Email", cliente.Email },
                    { "Nome",  cliente.Nome },
                    { "Documento", cliente.Documento },
                    { "Ativo", cliente.Ativo },
                    { "Sexo", cliente.Sexo },
                    { "EstadoCivilId", cliente.EstadoCivil.Id },
                    { "DataNascimento", cliente.DataNascimento }
                }
            };
        }
        public static QueryParamsSQL CarregarTodosClientes()
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM \"cliente\"",
                Parametros = new Dictionary<string, object>()
            };
        }
        public static QueryParamsSQL CarregarClientePorEmail(string email)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM \"cliente\" WHERE \"email\" = @Email",
                Parametros = new Dictionary<string, object> { { "Email", email } }
            };
        }
        public static QueryParamsSQL CarregarClientePorDocumento(string documento)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM \"cliente\" WHERE \"documento\" = @Documento",
                Parametros = new Dictionary<string, object> { { "Documento", documento } }
            };
        }
        public static QueryParamsSQL CarregarClientePorId(long id)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM \"cliente\" WHERE \"idCliente\" = @Id",
                Parametros = new Dictionary<string, object> { { "Id", id } }
            };
        }
        public static QueryParamsSQL CarregarClientePorNome(string nome)
        {
            return new QueryParamsSQL
            {
                Sql = "SELECT * FROM \"cliente\" WHERE \"Nome\" ILIKE @Nome",
                Parametros = new Dictionary<string, object> { { "Nome", $"%{nome}%" } }
            };
        }
    }
}
