using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RastreamentoPedidos.Data;
using RastreamentoPedidos.Data.Interface;
using RastreamentoPedidos.Middleware;
using RastreamentoPedidos.RastreamentoEncomendaHub;
using RastreamentoPedidos.Repositories.ClienteRepository;
using RastreamentoPedidos.Repositories.Interface.ICliente;

namespace StartapRastreamentoPedidos
{
    public class RastreamentoPedidos
    {
        public RastreamentoPedidos(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(option => option.AddPolicy("AllowAll", build =>
            {
                build.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
            services.AddSignalR();
            services.AddScoped<IDapperContext, DapperContext>();
            services.AddControllers().AddNewtonsoftJson(option =>
            {
                option.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddAuthentication();
            services.AddAuthorization();
            services.AddHealthChecks();
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<ICidadeRepository, ClienteRepositoryDapper>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<ITelefoneRepository, TelefoneRepository>();
            services.AddScoped<ITpLogradouroRepository, TpLogradouroRepository>();
            services.AddScoped<IUFRepository, UFRepository>();

            services.AddDbContext<RastreamentoPedidosContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("Default"),
                                assembly => assembly.MigrationsAssembly(typeof(RastreamentoPedidosContext).Assembly.FullName))
                                .EnableSensitiveDataLogging()
                                .EnableDetailedErrors();
            });
            services.AddSwaggerGen(c =>
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Rastreamento Pedido", Version = "v1" }));

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rastreamento de pedidos v1"));
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rastreamento de pedidos v1"));
            }
            app.UseErrorHandlingMiddleware();
            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<RastreamentoHub>("/rastreamentoHub");
            });
        }
    }
}
