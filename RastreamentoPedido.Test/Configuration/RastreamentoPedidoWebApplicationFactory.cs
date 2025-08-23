using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using RastreamentoPedidos.API.Data;
using System.Data.Common;

namespace RastreamentoPedido.Test.Configuration
{
    public class RastreamentoPedidoWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly IConfiguration _configuration;

        public RastreamentoPedidoWebApplicationFactory()
        {
            _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                //.AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public IConfiguration Configuration => _configuration;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureTestServices(services =>
            {
                var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<RastreamentoPedidosContext>));
                if (dbContextDescriptor != null)
                {
                    services.Remove(dbContextDescriptor);
                }

                services.AddSingleton<DbConnection>(container =>
                {
                    var connection = new NpgsqlConnection(GetConnectionString());
                    connection.Open();
                    return connection;
                });

                services.AddDbContext<RastreamentoPedidosContext>((container, options) =>
                {
                    var connection = container.GetRequiredService<DbConnection>();
                    options.UseNpgsql(connection);
                });
            });
            builder.UseEnvironment("Testing");
            base.ConfigureWebHost(builder);
        }

        public string? GetConnectionString()
        {
            var connString = _configuration.GetConnectionString("Default");
            return connString;
        }
    }
    
}
