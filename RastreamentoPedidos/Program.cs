using RastreamentoPedido.Core.Helpers;
using RastreamentoPedidos.API.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.SetDefaultConfiguration(builder.Environment);
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration(builder.Environment);

if (builder.Environment.EnvironmentName != "Testing")
{
    builder.WebHost
        .UseContentRoot(Directory.GetCurrentDirectory())
        .UseIISIntegration();
}

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentityConfig.CreateRoles(services);
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await IdentityConfig.CreateStatusOrder(services);
}

app.UseSwaggerConfiguration(builder.Environment);
app.UseApiConfiguration();

app.Run();

public partial class Program { }