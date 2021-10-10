using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Sentry.AspNetCore;

namespace Saitynas_API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseSentry(ConfigureOptions);
                        webBuilder.UseStartup<Startup>();
                    });
        }

        private static void ConfigureOptions(SentryAspNetCoreOptions o)
        {
            o.Dsn = "https://97fccb9ade0b4eafa87c78ea5061d5ce@o1034183.ingest.sentry.io/6000751";
            o.Debug = true;
            o.TracesSampleRate = 1.0;
        }
    }
}
