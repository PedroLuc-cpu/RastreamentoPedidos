using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RastreamentoPedidos.Data;
using RastreamentoPedido.Core.Configuration;
using RastreamentoPedido.WebApi.Core.Identidade;
using RastreamentoPedidos.Model;
using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model.Encomenda;

namespace RastreamentoPedidos.API.Configuration
{
    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RastreamentoPedidosContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString("Default"),
                                assembly => assembly.MigrationsAssembly(typeof(RastreamentoPedidosContext).Assembly.FullName))
                                .EnableSensitiveDataLogging()
                                .EnableDetailedErrors();
            });

            //services.AddScoped<RastreamentoPedidosContext>();

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddEntityFrameworkStores<RastreamentoPedidosContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });

            services.AddJwtConfiguration();

            //CreateRoles(services.BuildServiceProvider()).Wait();

            return services;

        }

        public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
            return app;
        }

        // voltar para privando quando for para produção
        public static async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] rolesNames = { Roles.Administrador, Roles.Usuario, Roles.Transportadora, Roles.Entregador, Roles.Gerente };
            IdentityResult result;
            foreach (var namesRole in rolesNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(namesRole);
                if (!roleExist)
                {
                    result = await roleManager.CreateAsync(new IdentityRole(namesRole));
                }
            }
        }

        public static async Task CreateStatusOrder(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<RastreamentoPedidosContext>();
            if (!context.StatusEncomendas.Any())
            {
                var statusList = new List<StatusEncomenda>
                {
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.AguardandoPagamento) },
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.PagamentoConfirmado) },
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.ProcessandoPedido) },
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.EnviadoParaTransportadora) },
                    new     StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.EmTransito) },
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.SaiuParaEntrega) },
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.TentativaDeEntrega) },
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.AguardandoRetirada) },
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.Cancelado) },
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.Devolvido) },
                    new StatusEncomenda { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.Extraviado) }
                };
                context.StatusEncomendas.AddRange(statusList);
                await context.SaveChangesAsync();
            }
        }
    }
}
