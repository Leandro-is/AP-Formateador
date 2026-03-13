using IS.DocumenFormater.Repository.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IS.DocumenFormater.Repository.Exchange
{
    public class EventLogRepository : GenericRepository<EventLog>, IEventLogRepository
    {
        public EventLogRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public override IQueryable<EventLog> Query() => DbContext.EventLogs.AsQueryable();

        public override IQueryable<EventLog> Query<TKey>(Expression<Func<EventLog, TKey>> exprOrder, bool ascending = true) => DbContext.EventLogs.OrderBy(exprOrder, ascending).AsQueryable();

        public override IQueryable<EventLog> QueryExp(Expression<Func<EventLog, bool>> expr) => DbContext.EventLogs.Where(expr).AsQueryable();

        public override IQueryable<EventLog> QueryExp<TKey>(Expression<Func<EventLog, bool>> expr, Expression<Func<EventLog, TKey>> exprOrder, bool ascending = true) => DbContext.EventLogs.Where(expr).OrderBy(exprOrder, ascending).AsQueryable();
    }
}
