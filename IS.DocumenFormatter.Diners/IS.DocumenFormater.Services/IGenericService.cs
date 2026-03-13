using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IS.DocumenFormater.Services
{
    public interface IGenericService<T>
    {
        Task<IList<T>> GetPageAsync(int from, int size);

        Task<IList<T>> GetAllAsync();

        Task<IList<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> exprOrder, bool ascending = true);

        Task<IList<T>> GetAllByCreatorUserIdAsync(Guid CreatorUserId);

        Task<IList<T>> GetAllWhereAsync(Expression<Func<T, bool>> expr);

        Task<IList<T>> GetAllWhereAsync<TKey>(Expression<Func<T, bool>> expr, Expression<Func<T, TKey>> exprOrder, bool ascending = true);

        Task<T> GetByIdAsync(int Id);

        Task<T> GetByIdAsync(Guid Id);

        Task<T> GetByExpAsync(Expression<Func<T, bool>> expr);

        T Insert(T entity);

        Task<T> InsertAsync(T entity);

        Task UpdateAsync(T entity);

        Task UpdateAsync(T entity, bool onlyModified);

        Task UpdateAsync(T entity, params Expression<Func<T, object>>[] updatedProperties);

        Task UpdateAsync(T entity, bool onlyModified = false, params Expression<Func<T, object>>[] updatedProperties);

        Task DeleteAsync(T entity);

        Task<bool> ExistAsync(Expression<Func<T, bool>> expr);

        Task<int> CountAsync();

        Task<int> Count(Expression<Func<T, bool>> expr);
    }
}
