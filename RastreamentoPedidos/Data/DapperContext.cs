
using System.Data;
using Npgsql;
using RastreamentoPedidos.Data.Interface;

namespace RastreamentoPedidos.Data
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IDbConnection ConnectionCreate()
        {
            return new NpgsqlConnection(_configuration.GetConnectionString("Default"));
        }

        public IDbConnection ConnectionCreate(string ConnectionStringName)
        {
            return new NpgsqlConnection(_configuration.GetConnectionString(ConnectionStringName));
        }
    }
}