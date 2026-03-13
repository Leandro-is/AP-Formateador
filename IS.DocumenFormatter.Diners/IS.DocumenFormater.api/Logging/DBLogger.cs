using IS.DocumenFormater.Repository;
using IS.DocumenFormater.Repository.Domain;
using IS.DocumenFormater.Repository.Exchange;
using IS.DocumenFormater.Services.Exchange;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace IS.DocumenFormater.api.Logging
{
    public class DBLogger : ILogger
    {
        private readonly IConfiguration _configuration;
        //private readonly IEventLogService _eventLogService;
        private readonly LoggerConfiguration _config;
        private string _categoryName;
        private bool _selfException = false;

        public DBLogger(string categoryName, LoggerConfiguration config, IConfiguration configuration)//)IEventLogService eventLogService)
        {
            _categoryName = categoryName;
            _config = config;
            //_eventLogService = eventLogService;
            _configuration = configuration;
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
                DBLoggerTrace dbLoggerTrace = null;
                var objmessage = message.Split("|||||");

                if (objmessage.Length > 1)
                {
                    dbLoggerTrace = JsonConvert.DeserializeObject<DBLoggerTrace>(objmessage[0].Replace("&quotbs;", "{").Replace("&quotbe;", "}"));
                    message = objmessage[1];
                }

                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DB_Connection_SingBox"));
                using (ApplicationDbContext applicationDbContext = new ApplicationDbContext(optionsBuilder.Options))
                {
                    EventLogRepository eventLogRepository = new EventLogRepository(applicationDbContext);
                    EventLogService eventLogService = new EventLogService(eventLogRepository);
                    eventLogService.Insert(new EventLog
                    {
                        Message = message,
                        EventId = eventId.Id,
                        LogLevel = logLevel.ToString(),
                        CreationDate = DateTime.UtcNow,
                        EntityName = dbLoggerTrace != null ? dbLoggerTrace.EntityName : "",
                        EntityId = dbLoggerTrace != null ? dbLoggerTrace.EntityId : "",
                        EntityField = dbLoggerTrace != null ? dbLoggerTrace.EntityField : "",
                        EntityValue = dbLoggerTrace != null ? dbLoggerTrace.EntityValue : ""
                    });
                }

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
            catch (Exception ex) { }
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            return true;// logLevel == _config.LogLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

    }
}
