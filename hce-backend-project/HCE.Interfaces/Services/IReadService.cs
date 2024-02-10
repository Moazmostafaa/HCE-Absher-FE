using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Services
{
    public interface IReadService<T> where T : class
    {
        #region Get Entity

        #region Not Async
        T GetById(object id, Expression<Func<T, object>> includeExpression = null);
        T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T GetSingle(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        T GetAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        #endregion

        #region Async
        Task<T> GetByIdAsync(object id, Expression<Func<T, object>> includeExpression = null);
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        Task<T> GetAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        #endregion

        #endregion

        #region Get Many

        #region Not Async
        IQueryable<T> GetMany();
        IQueryable<T> GetMany(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetMany(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        IQueryable<T> GetMany(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

        IQueryable<T> GetManyAsNoTracking();
        IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetManyAsNoTracking(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
           string orderBy, string orderDirection = "asc");
        IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize);
        IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
           int pageNumber, int pageSize, string orderBy, string orderDirection = "asc");
        #endregion

        #region Async
        //Task<IQueryable<T>> GetManyAsync();
        //Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> predicate);
        //Task<IQueryable<T>> GetManyAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        //Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include);

        //Task<IQueryable<T>> GetManyAsNoTrackingAsync();
        //Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate);
        //Task<IQueryable<T>> GetManyAsNoTrackingAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include);
        //Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        //Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);
        //Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        //    string orderBy, string orderDirection = "asc");
        //Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize);
        //Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include,
        //    int pageNumber, int pageSize, string orderBy, string orderDirection = "asc");
        #endregion

        #endregion

        #region Count
        int Count();
        int Count(Expression<Func<T, bool>> predicate);
        Task<int> CountAsync();
        Task<int> CountAsync(Expression<Func<T, bool>> predicate);
        #endregion
    }
}
