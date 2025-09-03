using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RastreamentoPedido.Core.Configuration;
using RastreamentoPedidos.Model;
using RastreamentoPedido.Core.DomainObjects;
using RastreamentoPedido.Core.Model.Encomenda;
using RastreamentoPedidos.API.Data;

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

            services.AddHealthChecks()
                    .AddNpgSql(configuration.GetConnectionString("Default") ?? "", name: "PostgreSQL Check", tags: ["db", "data"]);

            services.AddHealthChecksUI()
                    .AddInMemoryStorage();

            services.AddScoped<RastreamentoPedidosContext>();

            //services.AddDefaultIdentity<ApplicationUser>()
            //    .AddRoles<IdentityRole>()
            //    .AddErrorDescriber<IdentityMensagensPortugues>()
            //    .AddEntityFrameworkStores<RastreamentoPedidosContext>()
            //    .AddDefaultTokenProviders();

            //services.Configure<IdentityOptions>(options =>
            //{
            //    options.Password.RequireDigit = false;
            //    options.Password.RequireLowercase = false;
            //    options.Password.RequireNonAlphanumeric = false;
            //    options.Password.RequireUppercase = false;
            //    options.Password.RequiredLength = 6;
            //    options.User.RequireUniqueEmail = true;
            //    options.SignIn.RequireConfirmedEmail = true;
            //});

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
                .AddErrorDescriber<IdentityMensagensPortugues>()
                .AddEntityFrameworkStores<RastreamentoPedidosContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "ApiCookieAuth";
                options.Cookie.HttpOnly = true;
                /// quando o https for obrigatório
                //options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SecurePolicy = CookieSecurePolicy.None;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.ExpireTimeSpan = TimeSpan.FromDays(1);
                options.SlidingExpiration = true;

                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = 401;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = 403;
                    return Task.CompletedTask;
                };

            });



            //services.AddJwtConfiguration();

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
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.AguardandoPagamento) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.PagamentoConfirmado) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.ProcessandoPedido) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.EnviadoParaTransportadora) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.EmTransito) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.SaiuParaEntrega) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.TentativaDeEntrega) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.AguardandoRetirada) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.Cancelado) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.Devolvido) },
                    new() { Status = StatusEntrega.StatusEncomendaEnumToStr(StatusEntregaEnum.Extraviado) }
                };
                context.StatusEncomendas.AddRange(statusList);
                await context.SaveChangesAsync();
            }
        }
    }
}
