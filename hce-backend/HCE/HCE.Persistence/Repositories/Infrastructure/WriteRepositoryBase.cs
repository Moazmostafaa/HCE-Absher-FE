using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFCore.BulkExtensions;
using System.Threading.Tasks;
using HCE.Interfaces.Repositories;

namespace HCE.Persistence.Repositories.Infrastructure
{
    public class WriteRepositoryBase<T> : IWriteRepository<T> where T : class
    {
        protected DbSet<T> dbSet;
        protected DbContext _dataBaseContext;

        public WriteRepositoryBase(DbContext context)
        {
            _dataBaseContext = context;
            dbSet = _dataBaseContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Add(List<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task AddAsync(List<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public T Update(T entity)
        {
            entity = dbSet.Update(entity).Entity;
            return entity;
        }

        public void Update(List<T> entities)
        {
            dbSet.UpdateRange(entities);
        }

        public virtual void Delete(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> objects = dbSet.Where(predicate).AsEnumerable();
            dbSet.RemoveRange(objects);
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void BulkInsert(List<T> entities)
        {
            _dataBaseContext.BulkInsert(entities);
        }

        public void BulkUpdate(List<T> entities)
        {
            _dataBaseContext.BulkUpdate(entities);
        }

        public virtual async Task BulkInsertAsync(List<T> entities)
        {
            await _dataBaseContext.BulkInsertAsync(entities);
        }

        public async Task BulkUpdateAsync(List<T> entities)
        {
            await _dataBaseContext.BulkUpdateAsync(entities);
        }
    }
}
