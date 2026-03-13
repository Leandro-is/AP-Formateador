using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;
using System;

namespace IS.DocumenFormater.Logging
{
    static public class DBLoggerExtensions
    {
        static public ILoggingBuilder AddDBLogger(this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, DBLoggerProvider>());
            return builder;
        }
        static public ILoggingBuilder AddDBLogger(this ILoggingBuilder builder, Action<FileLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.AddFileLogger();
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
