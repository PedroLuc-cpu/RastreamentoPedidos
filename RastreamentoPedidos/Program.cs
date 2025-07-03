using Microsoft.AspNetCore.Authentication.Cookies;
using RastreamentoPedido.Core.Helpers;
using RastreamentoPedidos.API.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(options => 
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.SaveTokens = true;
        options.Scope.Add("email");
        options.Scope.Add("profile");
        options.Scope.Add("openid");
    });
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