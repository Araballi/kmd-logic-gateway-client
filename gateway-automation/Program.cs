using Destructurama;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gateway.Automation
{
    public enum ExitCode
    {
        Success = 0,
        Help = 1,
        GenericError = 2,
        InvalidParameters = 3,
    }

    public class Program
    {
        public static Task<int> Main(string[] args)
        {
            var minLogLevelSwitch = new Serilog.Core.LoggingLevelSwitch();
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Literate)
                .Destructure.JsonNetTypes()
                .Enrich.FromLogContext()
                .Enrich.WithProperty("CoreLogicVersion", GatewayAutomationAppVersion.Current)
                .MinimumLevel.ControlledBy(minLogLevelSwitch)
                .CreateLogger();

            var config = new ConfigurationBuilder()
                    .AddInMemoryCollection(new[]
                    {
                        KeyValuePair.Create("<dummy>", "must exist to avoid issues"),
                    })
                    .AddCommandLine(args)
                    .Build()
                    .Get<CommandLineConfig>();

            Log.Information("Hello World!");
            Log.Information("{SubscriptionId} :", config.SubscriptionId.ToString());
            Log.Information("{Token} :", config.Token);
            Console.ReadLine();
            return Task.FromResult((int)ExitCode.Success);
        }
    }
}
