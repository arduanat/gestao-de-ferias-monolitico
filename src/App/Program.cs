using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sentry;

namespace App
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
                        o.Dsn = "https://45bb506aa66f4f06a20bc5033dc492ad@o981789.ingest.sentry.io/5974663";
                        o.Debug = true;
                        o.TracesSampleRate = 1.0;
                    });

                    webBuilder.UseStartup<Startup>();
                });
    }
}
