using IS.DocumenFormater.api.Logging;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Reflection;
using System;
using System.IO;
using IS.DocumenFormater.api.Factories;

namespace IS.DocumenFormater.api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      var currentPathBatch = Assembly.GetEntryAssembly().Location;
      var directory = System.IO.Path.GetDirectoryName(currentPathBatch);
      var pathLog = Path.Combine(directory, "Logs", "logs-init-.txt");
      Log.Logger = new Serilog.LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApiVersion", ApiVersionInformation.CurrentVersionApi)
        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        //.Enrich.WithExceptionDetails()
        .WriteTo.Console(
          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {ThreadId} {RequestPath} {RequestId} [{Level:u3}] {Message:lj} {NewLine}{Exception}"
        )
        .WriteTo.File(
          pathLog,
          rollingInterval: RollingInterval.Day,
          //outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {Properties} [{Level:u3}] {Message:lj} {NewLine}{Exception}"
          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {ThreadId} {RequestPath} {ApiVersion} {RequestId} [{Level:u3}] {Message:lj} {NewLine}{Exception}"
        )
        .CreateLogger();
      try
      {
        Log.Information("Starting up");
        CreateWebHostBuilder(args).Build().Run();
      }
      catch (Exception ex)
      {
        Log.Error(ex, "Application start-up failed");
      }
      finally
      {
        Log.CloseAndFlush();
      }
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseIISIntegration()
                  //.UseIISIntegration(o => { o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(60); })
                  //.UseKestrel(o => { o.Limits.KeepAliveTimeout = TimeSpan.FromMinutes(60); })
                  .ConfigureLogging(logging =>
                  {
                    logging.ClearProviders();
                  })
      .UseSerilog((context, configuration) =>
      {
        var currentPathBatch = Assembly.GetEntryAssembly().Location;
        var directory = System.IO.Path.GetDirectoryName(currentPathBatch);
        var pathLog = Path.Combine(directory, "Logs", "logs-.txt");

        configuration
        .ReadFrom.Configuration(context.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("ApiVersion", ApiVersionInformation.CurrentVersionApi)
        .Enrich.WithThreadId()
        .Enrich.WithThreadName()
        .WriteTo.Console(
          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {ThreadId} {RequestPath} {RequestId} {ApiVersion} [{Level:u3}] {Scope} {Message:lj} {NewLine}{Exception}"
        )
        .WriteTo.File(
          pathLog,
          rollingInterval: RollingInterval.Day,
          outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {ThreadId} {RequestPath} {RequestId} {ApiVersion} [{Level:u3}] {Scope} {Message:lj} {NewLine}{Exception}"
        //outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} {ThreadId} {RequestPath} {RequestId} [{Level:u3}] {Message:lj} {NewLine}{Exception}"
        );
      })
            .UseStartup<Startup>();
    //.ConfigureLogging(logging =>
    //{
    //  logging.ClearProviders();
    //  logging.AddDBLogger();
    //});
  }
}
