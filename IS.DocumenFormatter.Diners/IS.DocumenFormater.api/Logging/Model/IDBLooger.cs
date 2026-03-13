using Microsoft.Extensions.Logging;
using System;

namespace IS.DocumenFormater.api.Logging.Model
{
    public interface IDBLogger : ILogger
    {
        void Log<TState>(DBLoggerTrace dbLoggerTrace, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);
    }
    public interface IDBLogger<T> : ILogger<T>
    {
        void Log<TState>(DBLoggerTrace dbLoggerTrace, LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter);
    }
}
