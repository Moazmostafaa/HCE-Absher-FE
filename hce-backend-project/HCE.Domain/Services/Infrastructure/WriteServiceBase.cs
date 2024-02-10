using HCE.Interfaces.Repositories;
using HCE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Domain.Services.Infrastructure
{
    public class WriteServiceBase<T> : IWriteService<T> where T : class
    {
        #region Fields
        public IWriteRepository<T> _writeRepository;
        #endregion

        #region Constructor
        public WriteServiceBase(IWriteRepository<T> writeRepository)
        {
            _writeRepository = writeRepository;
        }
        #endregion

        public void Add(T entity)
        {
            _writeRepository.Add(entity);
        }

        public void Add(List<T> entities)
        {
            _writeRepository.Add(entities);
        }

        public async Task AddAsync(T entity)
        {
            await _writeRepository.AddAsync(entity);
        }

        public async Task AddAsync(List<T> entities)
        {
            await _writeRepository.AddAsync(entities);
        }

        public void Delete(T entity)
        {
            _writeRepository.Delete(entity);
        }

        public void Delete(Expression<Func<T, bool>> predicate)
        {
            _writeRepository.Delete(predicate);
        }

        public T Update(T entity)
        {
            return _writeRepository.Update(entity);
        }

        public void Update(List<T> entities)
        {
            _writeRepository.Update(entities);
        }

        public void BulkInsert(List<T> entities)
        {
            _writeRepository.BulkInsert(entities);
        }

        public void BulkUpdate(List<T> entities)
        {
            _writeRepository.BulkUpdate(entities);
        }

        public async Task BulkInsertAsync(List<T> entities)
        {
            await _writeRepository.BulkInsertAsync(entities);
        }

        public async Task BulkUpdateAsync(List<T> entities)
        {
            await _writeRepository.BulkUpdateAsync(entities);
        }
    }
}
