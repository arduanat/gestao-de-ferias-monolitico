using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sentry;

namespace Aplicacao
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SentrySdk.CaptureMessage("Hello Sentry");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSentry(o =>
                    {
                        o.Dsn = "https://d2c9daaa4f314eb9bb1dd63011edcf75@o981789.ingest.sentry.io/5936300";
                        o.Debug = true;
                        o.TracesSampleRate = 1.0;
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
