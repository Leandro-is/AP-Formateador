using IS.DocumenFormater.Repository.Domain;
using IS.DocumenFormater.Repository.Exchange;

namespace IS.DocumenFormater.Services.Exchange
{
    public class EventLogService : GenericService<EventLog>, IEventLogService
    {
        public EventLogService(IEventLogRepository repository) : base(repository)
        {
        }
    }
}
