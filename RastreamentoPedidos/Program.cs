using RastreamentoPedidos.API.Configuration;
//using System.Runtime.InteropServices;

//namespace StartapRastreamentoPedidos
//{
//    public class Program
//    {
//        public static void Main(string[] args)
//        {
//            CreateHostBuilder(args).Build().Run();
//        }

//        public static IHostBuilder CreateHostBuilder(string[] args) =>
//                  Host.CreateDefaultBuilder(args)
//                      .ConfigureLogging((context, logging) =>
//                      {
//                          logging.ClearProviders();
//                          logging.AddConsole();
//                          logging.AddDebug();
//                          if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
//                          {
//                              logging.AddEventLog();
//                          }
//                      })
//                      .ConfigureWebHostDefaults(webBuilder =>
//                      {
//                          webBuilder.UseStartup<RastreamentoPedidos>()
//                          .UseKestrel(options =>
//                          {
//                              options.Limits.MaxRequestBodySize = 1073741824;
//                          });
//                      });
//    }
//}
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

app.UseSwaggerConfiguration(builder.Environment);
app.UseApiConfiguration();

app.Run();

public partial class Program { }