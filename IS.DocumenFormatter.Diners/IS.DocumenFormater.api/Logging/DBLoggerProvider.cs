using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace IS.DocumenFormater.api.Logging
{
    public class DBLoggerProvider : ILoggerProvider
    {
        private readonly IConfiguration _configuration;
        private readonly LoggerConfiguration _config;
        Dictionary<string, int> Lengths = new Dictionary<string, int>();
        ConcurrentQueue<LogEntry> InfoQueue = new ConcurrentQueue<LogEntry>();
        private readonly ConcurrentDictionary<string, DBLogger> _loggers = new ConcurrentDictionary<string, DBLogger>();

        public DBLoggerProvider(LoggerConfiguration config, IConfiguration configuration)// Func<string, LogLevel, bool> filter)
        {
            _configuration = configuration;
            _config = config;
        }
        public ILogger CreateLogger(string categoryName)
        {
            return _loggers.GetOrAdd(categoryName, name => new DBLogger(categoryName, _config, _configuration));
            //return new DBLogger(categoryName, _config, _configuration);
        }

        //public override bool IsEnabled(LogLevel logLevel)
        //{
        //    return true;
        //}

        //public override void WriteLog(LogEntry Info)
        //{
        //    InfoQueue.Enqueue(Info);
        //}

        public void Dispose()
        {
            _loggers.Clear();
        }
    }
}
