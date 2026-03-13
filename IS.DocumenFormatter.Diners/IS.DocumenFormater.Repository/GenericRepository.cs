using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IS.DocumenFormater.Repository
{
    public abstract class GenericRepository<T> : IGenericRepository<T>
        where T : class, new()
    {
        protected ApplicationDbContext DbContext { get; set; }

        public virtual async Task<T> FindByAsync(Expression<Func<T, bool>> expr) => await DbContext.Set<T>().FirstOrDefaultAsync(expr);

        public virtual async Task<T> GetAsync(int id) => await DbContext.FindAsync<T>(id);

        public virtual async Task<T> GetAsync(Guid id) => await DbContext.FindAsync<T>(id);

        public virtual IQueryable<T> Page(int from, int size) => DbContext.Set<T>().AsQueryable().Skip(from).Take(size);

        public virtual IQueryable<T> Query() => DbContext.Set<T>().AsQueryable();

        public virtual IQueryable<T> Query<TKey>(Expression<Func<T, TKey>> exprOrder, bool ascending = true) => DbContext.Set<T>().OrderBy(exprOrder, ascending).AsQueryable();

        public virtual IQueryable<T> QueryExp(Expression<Func<T, bool>> expr) => DbContext.Set<T>().Where(expr).AsQueryable();

        public virtual IQueryable<T> QueryExp<TKey>(Expression<Func<T, bool>> expr, Expression<Func<T, TKey>> exprOrder, bool ascending = true) => DbContext.Set<T>().Where(expr).OrderBy(exprOrder, ascending).AsQueryable();

        public T Insert(T entity)
        {
            var ee = DbContext.Set<T>().Add(entity);
            DbContext.SaveChanges();
            return ee.Entity;
        }

        public async Task<T> InsertAsync(T entity)
        {
            var ee = DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
            return ee.Entity;
        }

        public async Task UpdateAsync(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity, bool onlyModified) => await UpdateAsync(entity, onlyModified);

        public async Task UpdateAsync(T entity, params Expression<Func<T, object>>[] updatedProperties) => await UpdateAsync(entity, false, updatedProperties);

        public async Task UpdateAsync(T entity, bool onlyModified = false, params Expression<Func<T, object>>[] updatedProperties)
        {
            var dbEntityEntry = DbContext.Entry(entity);

            if (updatedProperties.Any())
            {
                var members = updatedProperties.SelectMany(selector => selector.GetPropertyAccesses());
                //foreach (var property in dbEntityEntry.OriginalValues.Properties) dbEntityEntry.Property(property.Name).IsModified = false;
                foreach (var member in members)
                {
                    var propertyName = member.Member.Name;
                    if (!onlyModified) dbEntityEntry.Property(propertyName).IsModified = true;
                    else
                    {
                        Type propertyType = dbEntityEntry.OriginalValues.Properties.First(x => x.Name == propertyName).PropertyInfo.PropertyType;
                        bool isDifferent = CompareOriginalCurrent(dbEntityEntry, propertyName);
                        dbEntityEntry.Property(propertyName).IsModified = isDifferent;
                    }
                }
                //dbEntityEntry.State = EntityState.Modified;
                await DbContext.SaveChangesAsync();
            }
            else if (onlyModified)
            {
                foreach (var property in dbEntityEntry.OriginalValues.Properties)
                {
                    //var original = dbEntityEntry.OriginalValues.GetValue<object>(property.Name);
                    //var current = dbEntityEntry.CurrentValues.GetValue<object>(property.Name);
                    bool isDifferent = CompareOriginalCurrent(dbEntityEntry, property.Name);
                    dbEntityEntry.Property(property.Name).IsModified = isDifferent;
                }
                await DbContext.SaveChangesAsync();
            }
            else
            {
                await UpdateAsync(entity);
            }
        }

        public async Task DeleteAsync(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }
        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            DbContext.Set<T>().RemoveRange(entities);
            await DbContext.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(Expression<Func<T, bool>> expr) => await DbContext.Set<T>().AnyAsync(expr);

        public async Task<int> Count() => await DbContext.Set<T>().CountAsync();

        public async Task<int> Count(Expression<Func<T, bool>> expr) => await DbContext.Set<T>().Where(expr).CountAsync();

        private bool CompareOriginalCurrent(EntityEntry<T> dbEntityEntry, String propertyName)
        {
            Type propertyType = dbEntityEntry.OriginalValues.Properties.First(x => x.Name == propertyName).PropertyInfo.PropertyType;
            var proposedValues = dbEntityEntry.CurrentValues;
            var databaseValues = dbEntityEntry.GetDatabaseValues();

            var original = databaseValues[propertyName];
            var current = proposedValues[propertyName];

            //var original = propertyType == typeof(Enum) ? dbEntityEntry.OriginalValues.GetValue<Enum>(propertyName) :
            //    propertyType == typeof(String) ? dbEntityEntry.OriginalValues.GetValue<String>(propertyName) :
            //    propertyType == typeof(Char) ? dbEntityEntry.OriginalValues.GetValue<Char>(propertyName) :
            //    propertyType == typeof(Guid) ? dbEntityEntry.OriginalValues.GetValue<Guid>(propertyName) :
            //    propertyType == typeof(Boolean) ? dbEntityEntry.OriginalValues.GetValue<Boolean>(propertyName) :
            //    propertyType == typeof(Byte) ? dbEntityEntry.OriginalValues.GetValue<Byte>(propertyName) :
            //    propertyType == typeof(Int16) ? dbEntityEntry.OriginalValues.GetValue<Int16>(propertyName) :
            //    propertyType == typeof(Int32) ? dbEntityEntry.OriginalValues.GetValue<Int32>(propertyName) :
            //    propertyType == typeof(Int64) ? dbEntityEntry.OriginalValues.GetValue<Int64>(propertyName) :
            //    propertyType == typeof(Single) ? dbEntityEntry.OriginalValues.GetValue<Single>(propertyName) :
            //    propertyType == typeof(Double) ? dbEntityEntry.OriginalValues.GetValue<Double>(propertyName) :
            //    propertyType == typeof(Decimal) ? dbEntityEntry.OriginalValues.GetValue<Decimal>(propertyName) :
            //    propertyType == typeof(TimeSpan) ? dbEntityEntry.OriginalValues.GetValue<TimeSpan>(propertyName) :
            //    propertyType == typeof(SByte) ? dbEntityEntry.OriginalValues.GetValue<SByte>(propertyName) :
            //    propertyType == typeof(UInt16) ? dbEntityEntry.OriginalValues.GetValue<UInt16>(propertyName) :
            //    propertyType == typeof(UInt32) ? dbEntityEntry.OriginalValues.GetValue<UInt32>(propertyName) :
            //    propertyType == typeof(UInt64) ? dbEntityEntry.OriginalValues.GetValue<UInt64>(propertyName) :
            //    propertyType == typeof(DateTime) ? dbEntityEntry.OriginalValues.GetValue<DateTime>(propertyName) :
            //    propertyType == typeof(DateTimeOffset) ? dbEntityEntry.OriginalValues.GetValue<DateTimeOffset>(propertyName) :
            //    propertyType == typeof(Nullable<Char>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Char>>(propertyName)  :
            //    propertyType == typeof(Nullable<Guid>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Guid>>(propertyName)  :
            //    propertyType == typeof(Nullable<Boolean>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Boolean>>(propertyName)  :
            //    propertyType == typeof(Nullable<Byte>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Byte>>(propertyName)  :
            //    propertyType == typeof(Nullable<Int16>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Int16>>(propertyName)  :
            //    propertyType == typeof(Nullable<Int32>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Int32>>(propertyName)  :
            //    propertyType == typeof(Nullable<Int64>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Int64>>(propertyName)  :
            //    propertyType == typeof(Nullable<Single>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Single>>(propertyName)  :
            //    propertyType == typeof(Nullable<Double>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Double>>(propertyName)  :
            //    propertyType == typeof(Nullable<Decimal>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<Decimal>>(propertyName)  :
            //    propertyType == typeof(Nullable<SByte>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<SByte>>(propertyName)  :
            //    propertyType == typeof(Nullable<UInt16>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<UInt16>>(propertyName)  :
            //    propertyType == typeof(Nullable<UInt32>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<UInt32>>(propertyName)  :
            //    propertyType == typeof(Nullable<UInt64>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<UInt64>>(propertyName)  :
            //    propertyType == typeof(Nullable<DateTime>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<DateTime>>(propertyName)  :
            //    propertyType == typeof(Nullable<DateTimeOffset>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<DateTimeOffset>>(propertyName)  :
            //    propertyType == typeof(Nullable<TimeSpan>) ? dbEntityEntry.OriginalValues.GetValue<Nullable<TimeSpan>>(propertyName)  :
            //    dbEntityEntry.OriginalValues.GetValue<Object>(propertyName);

            //var current = propertyType == typeof(Enum) ? dbEntityEntry.CurrentValues.GetValue<Enum>(propertyName) :
            //    propertyType == typeof(String) ? dbEntityEntry.CurrentValues.GetValue<String>(propertyName) :
            //    propertyType == typeof(Char) ? dbEntityEntry.CurrentValues.GetValue<Char>(propertyName) :
            //    propertyType == typeof(Guid) ? dbEntityEntry.CurrentValues.GetValue<Guid>(propertyName) :
            //    propertyType == typeof(Boolean) ? dbEntityEntry.CurrentValues.GetValue<Boolean>(propertyName) :
            //    propertyType == typeof(Byte) ? dbEntityEntry.CurrentValues.GetValue<Byte>(propertyName) :
            //    propertyType == typeof(Int16) ? dbEntityEntry.CurrentValues.GetValue<Int16>(propertyName) :
            //    propertyType == typeof(Int32) ? dbEntityEntry.CurrentValues.GetValue<Int32>(propertyName) :
            //    propertyType == typeof(Int64) ? dbEntityEntry.CurrentValues.GetValue<Int64>(propertyName) :
            //    propertyType == typeof(Single) ? dbEntityEntry.CurrentValues.GetValue<Single>(propertyName) :
            //    propertyType == typeof(Double) ? dbEntityEntry.CurrentValues.GetValue<Double>(propertyName) :
            //    propertyType == typeof(Decimal) ? dbEntityEntry.CurrentValues.GetValue<Decimal>(propertyName) :
            //    propertyType == typeof(SByte) ? dbEntityEntry.CurrentValues.GetValue<SByte>(propertyName) :
            //    propertyType == typeof(UInt16) ? dbEntityEntry.CurrentValues.GetValue<UInt16>(propertyName) :
            //    propertyType == typeof(UInt32) ? dbEntityEntry.CurrentValues.GetValue<UInt32>(propertyName) :
            //    propertyType == typeof(UInt64) ? dbEntityEntry.CurrentValues.GetValue<UInt64>(propertyName) :
            //    propertyType == typeof(DateTime) ? dbEntityEntry.CurrentValues.GetValue<DateTime>(propertyName) :
            //    propertyType == typeof(DateTimeOffset) ? dbEntityEntry.CurrentValues.GetValue<DateTimeOffset>(propertyName) :
            //    propertyType == typeof(TimeSpan) ? dbEntityEntry.CurrentValues.GetValue<TimeSpan>(propertyName) :
            //    propertyType == typeof(Nullable<Char>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Char>>(propertyName) :
            //    propertyType == typeof(Nullable<Guid>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Guid>>(propertyName) :
            //    propertyType == typeof(Nullable<Boolean>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Boolean>>(propertyName) :
            //    propertyType == typeof(Nullable<Byte>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Byte>>(propertyName) :
            //    propertyType == typeof(Nullable<Int16>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Int16>>(propertyName) :
            //    propertyType == typeof(Nullable<Int32>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Int32>>(propertyName) :
            //    propertyType == typeof(Nullable<Int64>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Int64>>(propertyName) :
            //    propertyType == typeof(Nullable<Single>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Single>>(propertyName) :
            //    propertyType == typeof(Nullable<Double>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Double>>(propertyName) :
            //    propertyType == typeof(Nullable<Decimal>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<Decimal>>(propertyName) :
            //    propertyType == typeof(Nullable<SByte>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<SByte>>(propertyName) :
            //    propertyType == typeof(Nullable<UInt16>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<UInt16>>(propertyName) :
            //    propertyType == typeof(Nullable<UInt32>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<UInt32>>(propertyName) :
            //    propertyType == typeof(Nullable<UInt64>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<UInt64>>(propertyName) :
            //    propertyType == typeof(Nullable<DateTime>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<DateTime>>(propertyName) :
            //    propertyType == typeof(Nullable<DateTimeOffset>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<DateTimeOffset>>(propertyName) :
            //    propertyType == typeof(Nullable<TimeSpan>) ? dbEntityEntry.CurrentValues.GetValue<Nullable<TimeSpan>>(propertyName) :
            //    dbEntityEntry.CurrentValues.GetValue<Object>(propertyName);

            return ((original == null && current != null) || (original != null && !original.Equals(current)));
        }
    }
}
