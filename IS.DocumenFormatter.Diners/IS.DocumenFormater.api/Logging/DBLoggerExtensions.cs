using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace IS.DocumenFormater.api.Logging
{
    static public class DBLoggerExtensions
    {
        static public ILoggingBuilder AddDBLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();


            var config = new LoggerConfiguration
            {
                LogLevel = LogLevel.Information,
                Color = ConsoleColor.Red
            };
            var _configuration = builder.Services.BuildServiceProvider().GetService<IConfiguration>();
            builder.AddProvider(new DBLoggerProvider(config, _configuration));

            //builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, DBLoggerProvider>());
            return builder;
        }
        //static public ILoggingBuilder AddDBLogger(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
        //{
        //    if (configure == null)
        //    {
        //        throw new ArgumentNullException(nameof(configure));
        //    }

        //    builder.AddDBLogger();
        //    builder.Services.Configure(configure);

        //    return builder;
        //}
    }
}
