using HCE.Interfaces.Repositories;
using HCE.Interfaces.Services;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Services.Infrastructure
{
    public class ReadServiceBase<T> : IReadService<T> where T : class
    {
        #region Fields
        public IReadRepository<T> _readRepository;
        #endregion

        #region Constructor
        public ReadServiceBase(IReadRepository<T> readRepository)
        {
            _readRepository = readRepository;
        }
        #endregion

        #region Get Entity

        #region Not Async
        public T GetById(object id, Expression<Func<T, object>> include = null)
        {
            return _readRepository.GetById(id, include);
        }

        public T Get(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return _readRepository.Get(predicate, include);
        }

        public T GetAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return _readRepository.GetAsNoTracking(predicate, include);
        }

        public T GetSingle(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return _readRepository.GetSingle(predicate, include);
        }
        #endregion

        #region Async
        public async Task<T> GetByIdAsync(object id, Expression<Func<T, object>> include = null)
        {
            return await _readRepository.GetByIdAsync(id, include);
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await _readRepository.GetSingleAsync(predicate, include);
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await _readRepository.GetSingleAsync(predicate, include);
        }

        public async Task<T> GetAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return await _readRepository.GetAsNoTrackingAsync(predicate, include);
        }
        #endregion

        #endregion

        #region Get Many

        #region Not Async
        public IQueryable<T> GetMany()
        {
            return _readRepository.GetMany();
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> predicate)
        {
            return _readRepository.GetMany(predicate);
        }

        public IQueryable<T> GetMany(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return _readRepository.GetMany(include);
        }

        public IQueryable<T> GetMany(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return _readRepository.GetMany(predicate, include);
        }

        public IQueryable<T> GetManyAsNoTracking()
        {
            return _readRepository.GetManyAsNoTracking();
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate)
        {
            return _readRepository.GetManyAsNoTracking(predicate);
        }

        public IQueryable<T> GetManyAsNoTracking(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        {
            return _readRepository.GetManyAsNoTracking(include);
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            return _readRepository.GetManyAsNoTracking(predicate, include);
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return _readRepository.GetManyAsNoTracking(predicate, include, orderBy);
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, string orderBy, string orderDirection = "asc")
        {
            return _readRepository.GetManyAsNoTracking(predicate, include, orderBy, orderDirection);
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize)
        {
            return _readRepository.GetManyAsNoTracking(predicate, include, orderBy, pageNumber, pageSize);
        }

        public IQueryable<T> GetManyAsNoTracking(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, int pageNumber, int pageSize, string orderBy, string orderDirection = "asc")
        {
            return _readRepository.GetManyAsNoTracking(predicate, include, pageNumber, pageSize, orderBy, orderDirection);
        }
        #endregion

        #region Async
        //public async Task<IQueryable<T>> GetManyAsync()
        //{
        //    return await _readRepository.GetManyAsync();
        //}

        //public async Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> predicate)
        //{
        //    return await _readRepository.GetManyAsync(predicate);
        //}

        //public async Task<IQueryable<T>> GetManyAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        //{
        //    return await _readRepository.GetManyAsync(include);
        //}

        //public async Task<IQueryable<T>> GetManyAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        //{
        //    return await _readRepository.GetManyAsync(predicate, include);
        //}

        //public async Task<IQueryable<T>> GetManyAsNoTrackingAsync()
        //{
        //    return await _readRepository.GetManyAsNoTrackingAsync();
        //}

        //public async Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate)
        //{
        //    return await _readRepository.GetManyAsNoTrackingAsync(predicate);
        //}

        //public async Task<IQueryable<T>> GetManyAsNoTrackingAsync(Func<IQueryable<T>, IIncludableQueryable<T, object>> include)
        //{
        //    return await _readRepository.GetManyAsNoTrackingAsync(include);
        //}

        //public async Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        //{
        //    return await _readRepository.GetManyAsNoTrackingAsync(predicate, include);
        //}

        //public async Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        //{
        //    return await _readRepository.GetManyAsNoTrackingAsync(predicate, include, orderBy);
        //}

        //public async Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, string orderBy, string orderDirection = "asc")
        //{
        //    return await _readRepository.GetManyAsNoTrackingAsync(predicate, include, orderBy, orderDirection);
        //}

        //public async Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, int pageNumber, int pageSize)
        //{
        //    return await _readRepository.GetManyAsNoTrackingAsync(predicate, include, orderBy, pageNumber, pageSize);
        //}

        //public async Task<IQueryable<T>> GetManyAsNoTrackingAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include, int pageNumber, int pageSize, string orderBy, string orderDirection = "asc")
        //{
        //    return await _readRepository.GetManyAsNoTrackingAsync(predicate, include, pageNumber, pageSize, orderBy, orderDirection);
        //}
        #endregion

        #endregion

        #region Count
        public int Count()
        {
            return _readRepository.Count();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _readRepository.Count(predicate);
        }

        public async Task<int> CountAsync()
        {
            return await _readRepository.CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _readRepository.CountAsync(predicate);
        }
        #endregion
    }
}
