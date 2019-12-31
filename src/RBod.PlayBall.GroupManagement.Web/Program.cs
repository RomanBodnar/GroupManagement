using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Config;
using NLog.Fluent;
using NLog.Targets;
using NLog.Web;
using LogLevel = NLog.LogLevel;

namespace RBod.PlayBall.GroupManagement.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConfigureNLog();
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders(); // will be set up by NLog
                    builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace); //
                })
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        // TODO: replace with nlog.config
        private static void ConfigureNLog()
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ColoredConsoleTarget("coloredConsole")
            {
                Layout = @"${date:format=HH\:mm\:ss} ${logger} ${level} ${message} ${exception}"
            };

            //var fileTarget = new FileTarget("file")
            //{
            //    FileName = "${baseDir}/file.log",
            //    Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception} ${ndlc}"
            //};

            //config.AddTarget(fileTarget);
            config.AddTarget(consoleTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Info, consoleTarget, "RBod.*");
            config.AddRule(LogLevel.Warn, LogLevel.Fatal, consoleTarget);
            //config.AddRule(LogLevel.Warn, LogLevel.Fatal, fileTarget);
            LogManager.Configuration = config;
        }
    }
}
