using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IS.DocumenFormater.Repository
{
    public interface IGenericRepository<T>
    {
        Task<T> FindByAsync(Expression<Func<T, bool>> expr);

        Task<T> GetAsync(int id);

        Task<T> GetAsync(Guid id);

        IQueryable<T> Page(int from, int size);

        IQueryable<T> Query();

        IQueryable<T> Query<TKey>(Expression<Func<T, TKey>> exprOrder, bool ascending = true);

        IQueryable<T> QueryExp(Expression<Func<T, bool>> expr);

        IQueryable<T> QueryExp<TKey>(Expression<Func<T, bool>> expr, Expression<Func<T, TKey>> exprOrder, bool ascending = true);

        T Insert(T entity);

        Task<T> InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task UpdateAsync(T entity, bool onlyModified);

        Task UpdateAsync(T entity, params Expression<Func<T, object>>[] updatedProperties);

        Task UpdateAsync(T entity, bool onlyModified = false, params Expression<Func<T, object>>[] updatedProperties);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(Expression<Func<T, bool>> expr);

        Task<int> Count();

        Task<int> Count(Expression<Func<T, bool>> expr);
    }
}
