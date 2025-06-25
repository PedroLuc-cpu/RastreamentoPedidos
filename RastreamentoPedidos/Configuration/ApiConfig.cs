using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using RastreamentoPedido.Core.Repositories.Clientes;
using RastreamentoPedidos.Repositories.ClienteRepository;
using RastreamentoPedidos.API.Configuration.ModelBinders;
using RastreamentoPedido.Core.Converters;
using Microsoft.AspNetCore.HttpOverrides;
using RastreamentoPedido.Core.Data;
using RastreamentoPedidos.Data;
using RastreamentoPedidos.API.Hubs;
using RastreamentoPedido.Core.Repositories.IEstadoCivilRepository;
using RastreamentoPedidos.API.Repositories;
using RastreamentoPedido.Core.Service;
using RastreamentoPedidos.API.Services;
using RastreamentoPedido.Core.Repositories.Encomenda;
using RastreamentoPedidos.API.Repositories.Encomendas;

namespace RastreamentoPedidos.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            RegisterServices(services);
            services.AddHttpContextAccessor();
            services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0, new DateTimeModelBinderProvider());
                options.Filters.Add(new AuthorizeFilter(new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build()));
                //options.UseRoutePrefix("api/v1");
            }).AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
                options.JsonSerializerOptions.Converters.Add(new TimeOnlyConverter());
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = false;
            });

            services.AddSignalR();

            UserHandler.ConnectedUsers.Clear();
            UserHandler.UserSections.Clear();

            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowAnyHeader();
                });
            });

            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                options.KnownNetworks.Clear();
                options.KnownProxies.Clear();
            });

            services.AddEndpointsApiExplorer();

            return services;
        }

        public static WebApplication UseApiConfiguration(this WebApplication app)
        {
            app.MapHub<RastreamentoHub>("/rastreamentoHub");
            app.MapHub<NotificationHub>("/notificationsHub");

            app.UseCors("Total");

            app.Use(async (context, next) =>
            {
                var request = context.Request;

                if (request.Path.StartsWithSegments("/notificationsHub", StringComparison.OrdinalIgnoreCase) && request.Query.TryGetValue("access_token", out var accessToken))
                {
                    request.Headers.Append("Authorization", $"Bearer {accessToken}");
                }
                await next();
            });

            app.UseIdentityConfiguration();

            app.MapControllers();

            return app;
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddScoped<IDapperContext, DapperContext>();

            ///Clientes
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEstadoCivilRepository, EstadoCivilRepository>();
            // Endereço
            services.AddScoped<ICidadeRepository, CidadeRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<ITelefoneRepository, TelefoneRepository>();
            services.AddScoped<ITpLogradouroRepository, TpLogradouroRepository>();
            services.AddScoped<IUFRepository, UFRepository>();

            // Encomendas
            services.AddScoped<IStatusEncomendaRepository, StatusEncomendaRepository>();

            services.AddHttpClient<ICidadeService, CidadeServices>();
        }

        public static class UserHandler
        {
            public static IDictionary<string, string> ConnectedUsers = new Dictionary<string, string>();
            public static IDictionary<string, IList<string>> UserSections = new Dictionary<string, IList<string>>();
        }
    }
}
