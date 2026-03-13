using IS.DocumenFormater.Repository.Domain;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace IS.DocumenFormater.Repository.Exchange
{
    public class TransaccionalDocumentFormaterRepository : GenericRepository<TransaccionalDocumentFormater>, ITransaccionalDocumentFormaterRepository
    {
        public TransaccionalDocumentFormaterRepository(ApplicationDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public override IQueryable<TransaccionalDocumentFormater> Query() => DbContext.TransaccionalDocumentFormaters.AsQueryable();

        public override IQueryable<TransaccionalDocumentFormater> Query<TKey>(Expression<Func<TransaccionalDocumentFormater, TKey>> exprOrder, bool ascending = true) => DbContext.TransaccionalDocumentFormaters.OrderBy(exprOrder, ascending).AsQueryable();

        public override IQueryable<TransaccionalDocumentFormater> QueryExp(Expression<Func<TransaccionalDocumentFormater, bool>> expr) => DbContext.TransaccionalDocumentFormaters.Where(expr).AsQueryable();

        public override IQueryable<TransaccionalDocumentFormater> QueryExp<TKey>(Expression<Func<TransaccionalDocumentFormater, bool>> expr, Expression<Func<TransaccionalDocumentFormater, TKey>> exprOrder, bool ascending = true) => DbContext.TransaccionalDocumentFormaters.Where(expr).OrderBy(exprOrder, ascending).AsQueryable();
    }
}
