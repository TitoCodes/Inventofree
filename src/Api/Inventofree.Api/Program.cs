using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;

namespace Inventofree.Api;

public static class Program
{
    public static void Main(string[] args)
    {            
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        var config = new ConfigurationBuilder()
            .AddJsonFile($"appSettings.{environment}.json", optional: false)
            .Build();
            
        var builder = CreateHostBuilder(args);
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day, restrictedToMinimumLevel: LogEventLevel.Error)
            .WriteTo.MSSqlServer(
                connectionString: config.GetConnectionString("Default"),
                sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true},
                restrictedToMinimumLevel:LogEventLevel.Warning)
            .MinimumLevel.Information()
            .CreateLogger();
            
        builder.UseSerilog(); 
        builder.Build().Run();
    }

    private static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
}