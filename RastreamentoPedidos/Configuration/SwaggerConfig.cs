using Microsoft.OpenApi.Models;
using System.Reflection;

namespace RastreamentoPedidos.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IWebHostEnvironment hostEnvironment)
        {
            var versao = "";
            if (Assembly.GetExecutingAssembly().GetName().Version != null)
            {
                versao = Assembly.GetExecutingAssembly().GetName().Version!.ToString();
            }
            services.AddSwaggerGen(c =>
            {
                string ambiente = string.Empty;
                if (hostEnvironment.EnvironmentName != "Production")
                {
                    ambiente = hostEnvironment.EnvironmentName;
                }
                c.SwaggerDoc("auth-v1", GetOpenApiInfo(versao, ambiente));
                //outro exemplo de como pegar o xml do projeto


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                // c.IncludeXmlComments(xmlPath);

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Insira o token JWT desta maneira: Bearer {Seu token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
                //c.AddSignalRSwaggerGen(action =>
                //{
                //    action.DisplayInDocument("notification-hub-v1");
                //});
            });

            return services;
        }

        private static OpenApiInfo GetOpenApiInfo(string versao, string ambiente)
        {
            var info = new OpenApiInfo();
            info.Title = $"Rastreamente De Pedidos API";
            if (!string.IsNullOrEmpty(ambiente))
            {
                info.Title = $"Rastreamente De Pedidos HelpDesk API - {ambiente}";
            }

            info.Description = "Api de backend referente ao serviço de Rastreamente De Pedidos";

            info.Version = versao;
            info.Contact = new OpenApiContact
            {
                Name = "Pedro Lucas Santos",
                Email = "pedrolucas_gta2015@hotmail.com"
            };


            return info;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IWebHostEnvironment hostEnvironment)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/auth-v1/swagger.json", "Auth v1");
                if (hostEnvironment.EnvironmentName != "Production")
                {
                    c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
                }
            });

            return app;
        }
    }
}