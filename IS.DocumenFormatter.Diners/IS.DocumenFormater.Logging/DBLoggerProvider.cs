using Microsoft.Extensions.Logging;
using System;

namespace IS.DocumenFormater.Logging
{
    public class DBLoggerProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;

        public DBLoggerProvider(Func<string, LogLevel, bool> filter)
        {
            _filter = filter;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return new DBLogger(categoryName, _filter);
        }

        public void Dispose()
        {

        }
    }
}
