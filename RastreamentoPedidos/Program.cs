using System.Runtime.InteropServices;

namespace StartapRastreamentoPedidos
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
                  Host.CreateDefaultBuilder(args)
                      .ConfigureLogging((context, logging) =>
                      {
                          logging.ClearProviders();
                          logging.AddConsole();
                          logging.AddDebug();
                          if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                          {
                              logging.AddEventLog();
                          }
                      })
                      .ConfigureWebHostDefaults(webBuilder =>
                      {
                          webBuilder.UseStartup<RastreamentoPedidos>()
                          .UseKestrel(options =>
                          {
                              options.Limits.MaxRequestBodySize = 1073741824;
                          });
                      });
    }
}