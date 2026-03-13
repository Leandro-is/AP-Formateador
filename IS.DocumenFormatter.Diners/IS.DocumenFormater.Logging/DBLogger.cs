using Microsoft.Extensions.Logging;
using System;

namespace IS.DocumenFormater.Logging
{
    public class DBLogger : ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private bool _selfException = false;

        public DBLogger(string categoryName, Func<string, LogLevel, bool> filter)
        {
            _categoryName = categoryName;
            _filter = filter;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;
            if (_selfException)
            {
                _selfException = false;
                return;
            }
            _selfException = true;
            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }
            var message = formatter(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            if (exception != null)
            {
                message += "\n" + exception.ToString();
            }
            try
            {
                //message = message.Length > CustomLoggerDBContext.MessageMaxLength ?
                //    message.Substring(0, CustomLoggerDBContext.MessageMaxLength) : message;
                //_context.EventLog.Add(new EventLog
                //{
                //    Message = message,
                //    EventId = eventId.Id,
                //    LogLevel = logLevel.ToString(),
                //    CreatedTime = DateTime.UtcNow
                //});
                //_context.SaveChanges();
                _selfException = false;
            }
            catch { }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }
}
