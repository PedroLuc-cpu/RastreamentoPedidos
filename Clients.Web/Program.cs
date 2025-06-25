using Blazored.LocalStorage;
using Clients.Web.Components;
using Clients.Web.Contracts.Services.Authentication;
using Clients.Web.Providers;
using Clients.Web.Services.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMudServices();
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("http://localhost:7000")
});

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();

/****** Services - Inicio ******/
/* Authentication */
builder.Services.AddScoped<IAuthService, AuthService>();

/****** Services - Fim ******/









var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();