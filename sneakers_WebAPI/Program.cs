using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace sneakers_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.text", restrictedToMinimumLevel:
                Serilog.Events.LogEventLevel.Information,
                rollingInterval: RollingInterval.Day)
                .WriteTo.File("logs/errorlog.txt", restrictedToMinimumLevel:
                Serilog.Events.LogEventLevel.Warning)
                .CreateLogger();
                       
            try
            {
                Log.Information("Starting service...");
                CreateHostBuilder(args).Build().Run();
            }
            catch(System.Exception e)
            {
                Log.Information("Exiting service...");
                Log.Fatal(e, "Exception in Application");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            
            
            Host.CreateDefaultBuilder(args)
            .UseSerilog()

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
