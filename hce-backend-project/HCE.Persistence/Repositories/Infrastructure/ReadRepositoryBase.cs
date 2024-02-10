using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using HCE.Interfaces.Repositories;
using HCE.Utility.Extensions;

namespace HCE.Persistence.Repositories.Infrastructure
{
    public class ReadRepositoryBase<T> : IReadRepository<T> where T : class
    {
        private DbSet<T> dbSet;
        protected readonly DbContext _dataBaseContext;

        public ReadRepositoryBase(DbContext context)
        {
            _dataBaseContext = context;
            dbSet = _dataBaseContext.Set<T>();
        }

        #region Get Entity

        #region Not Async
        public T GetById(object id, Expression<Func<T, object>> includeExpression = null)
        {
            var item = dbSet.Find(id);
            if (includeExpression != null)
            {
                if (item == null)
                    return null;

                _dataBaseContext.Entry(item).Reference(includeExpression).Load();
            }
            return item;
        }

        public T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include).FirstOrDefault();
        }

        public T GetAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include).AsNoTracking().FirstOrDefault();
        }

        public T GetSingle(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include).SingleOrDefault();
        }
        #endregion

        #region Async
        public Task<T> GetByIdAsync(object id, Expression<Func<T, object>> includeExpression = null)
        {
            var item = dbSet.FindAsync(id).Result;
            if (includeExpression != null)
            {
                if (item == null)
                    return null;

                _dataBaseContext.Entry(item).Reference(includeExpression).LoadAsync();
            }

            return Task.FromResult(item);
        }

        public Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include).FirstOrDefaultAsync();
        }

        public Task<T> GetAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include).AsNoTracking().FirstOrDefaultAsync();
        }

        public Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include).SingleOrDefaultAsync();
        }
        #endregion

        #endregion

        #region Get Many

        #region Not Async
        public IQueryable<T> GetMany()
        {
            return this.GetManyHelper(null, null, null, null, null);
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            return this.GetManyHelper(predicate, null, null, null, null);
        }

        public IQueryable<T> GetMany(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return this.GetManyHelper(null, include, null, null, null);
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return GetQueryable(predicate, include);
        }

        public IQueryable<T> GetManyAsNoTracking()
        {
            return this.GetManyHelper(null, null, null, null, null).AsNoTracking();
        }
        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return this.GetManyHelper(predicate, null, null, null, null).AsNoTracking();
        }

        public IQueryable<T> GetManyAsNoTracking(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return this.GetManyHelper(null, include, null, null, null).AsNoTracking();
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return this.GetManyHelper(predicate, include, null, null, null).AsNoTracking();
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return this.GetManyHelper(predicate, include, orderBy, null, null).AsNoTracking();
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                                    string orderBy, string orderDirection = "asc")
        {
            return this.GetManyHelper(predicate, include, orderBy, orderDirection, null, null).AsNoTracking();
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize)
        {
            return this.GetManyHelper(predicate, include, orderBy, pageNumber, pageSize).AsNoTracking();
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
                                    int pageNumber, int pageSize, string orderBy, string orderDirection = "asc")
        {
            return this.GetManyHelper(predicate, include, orderBy, orderDirection, pageNumber, pageSize).AsNoTracking();
        }
        #endregion

        #endregion

        #region Count
        public int Count()
        {
            return dbSet.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Count(predicate);
        }

        public Task<int> CountAsync()
        {
            return dbSet.CountAsync();
        }

        public Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return dbSet.CountAsync(predicate);
        }
        #endregion

        #region Helper Methods
        private IQueryable<T> GetQueryable(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = dbSet;

            if (include != null)
                query = include(query);

            if (predicate != null)
                query = query.Where(predicate);

            return query;
        }

        private IQueryable<T> GetManyHelper(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
                query = orderBy(query);

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                pageNumber = pageNumber - 1;
                int skip = pageNumber.Value * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            return query;
        }

        private IQueryable<T> GetManyHelper(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                    string orderBy = null, string orderDirection = "asc", int? pageNumber = null, int? pageSize = null)
        {
            IQueryable<T> query = GetQueryable(predicate, include);

            if (orderBy != null)
                query = query.OrderBy(orderBy, orderDirection);

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                pageNumber = pageNumber - 1;
                int skip = pageNumber.Value * pageSize.Value;
                query = query.Skip(skip).Take(pageSize.Value);
            }

            return query;
        }
        public bool GetAny(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Any(predicate);
        }

        public async Task<bool> GetAnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }
        #endregion

        public Expression<Func<T, bool>> CombineExpressions(List<Expression<Func<T, bool>>> expressions)
        {
            expressions = expressions.Where(x => x != null).ToList();

            if (expressions.Count == 0)
                return v => true;
            else
            {
                Expression<Func<T, bool>> e = expressions[0];

                for (int i = 1; i < expressions.Count; i++)
                {
                    e = e.CombineWithAndAlso(expressions[i]);
                }

                return e;
            }
        }
    }
}
