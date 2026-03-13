using IS.DocumenFormater.Repository;
using IS.DocumenFormater.Repository.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IS.DocumenFormater.Services
{
    public abstract class GenericService<T> : IGenericService<T>
        where T : class, new()
    {
        protected GenericService(IGenericRepository<T> entityRepository)
        {
            EntityRepository = entityRepository;
        }

        protected IGenericRepository<T> EntityRepository { get; }

        public T Insert(T entity) => EntityRepository.Insert(entity);

        public async Task<T> InsertAsync(T entity) => await EntityRepository.InsertAsync(entity);

        public async Task UpdateAsync(T entity) => await EntityRepository.UpdateAsync(entity);

        public async Task UpdateAsync(T entity, bool onlyModified) => await UpdateAsync(entity, onlyModified);

        public async Task UpdateAsync(T entity, params Expression<Func<T, object>>[] updatedProperties) => await UpdateAsync(entity, false, updatedProperties);

        public async Task UpdateAsync(T entity, bool onlyModified = false, params Expression<Func<T, object>>[] updatedProperties) => await EntityRepository.UpdateAsync(entity, onlyModified, updatedProperties);

        public async Task<IList<T>> GetPageAsync(int from, int size) => await EntityRepository.Page(from, size).ToListAsync();

        public async Task<IList<T>> GetAllAsync() => await EntityRepository.Query().ToListAsync();

        public async Task<IList<T>> GetAllAsync<TKey>(Expression<Func<T, TKey>> exprOrder, bool ascending = true) => await EntityRepository.Query(exprOrder, ascending).ToListAsync();

        public async Task<IList<T>> GetAllByCreatorUserIdAsync(Guid CreatorUserId) => await EntityRepository.QueryExp(x => (x is IGeneric) && ((IGeneric)x).CreatorUserId == CreatorUserId).ToListAsync();

        public async Task<IList<T>> GetAllWhereAsync(Expression<Func<T, bool>> expr) => await EntityRepository.QueryExp(expr).ToListAsync();

        public async Task<IList<T>> GetAllWhereAsync<TKey>(Expression<Func<T, bool>> expr, Expression<Func<T, TKey>> exprOrder, bool ascending = true) => await EntityRepository.QueryExp(expr, exprOrder, ascending).ToListAsync();

        public async Task<T> GetByExpAsync(Expression<Func<T, bool>> expr) => await EntityRepository.FindByAsync(expr);

        public async Task<T> GetByIdAsync(int id) => await EntityRepository.GetAsync(id);

        public async Task<T> GetByIdAsync(Guid id) => await EntityRepository.GetAsync(id);

        public async Task DeleteAsync(T entity) => await EntityRepository.DeleteAsync(entity);

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> expr) => await EntityRepository.ExistAsync(expr);

        public async Task<int> CountAsync() => await EntityRepository.Count();

        public async Task<int> Count(Expression<Func<T, bool>> expr) => await EntityRepository.Count(expr);
    }
}
