using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Echo.Core.Configuration;
using Serilog;
using Serilog.Events;
using System;
using Serilog.Sinks.SpectreConsole;
using Serilog.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Serilog.Core;
using Microsoft.Extensions.Configuration;


namespace Echo.Loggers.Serilog
{
    public static class SerilogExtensions
    {
        private static IConfiguration _configuration;
        private static IServiceProvider _serviceProvider;
        public static Logger RegisterSerilog(IWebHost host)
        {
            _serviceProvider = host.Services;
            _configuration = _serviceProvider.GetRequiredService<IConfiguration>();
            var logOptions = _serviceProvider.GetRequiredService<IOptions<LogOptions>>().Value;
            var logLevel = Enum.TryParse<LogEventLevel>(logOptions.Level, true, out var level)
               ? level
               : LogEventLevel.Information;

            var logConfig = new LoggerConfiguration()
             .MinimumLevel.Is(logLevel)
               .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
               // Only show ef-core information in error level
               .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Error)
               // Filter out ASP.NET Core infrastructure logs that are Information and below
               .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
               .Enrich.WithExceptionDetails()
               .Enrich.FromLogContext()
               .WriteTo.Debug()
               .WriteTo.SpectreConsole(logOptions.LogTemplate, logLevel);

            return logConfig.ReadFrom.Configuration(_configuration).CreateLogger();
        }


    }
}
