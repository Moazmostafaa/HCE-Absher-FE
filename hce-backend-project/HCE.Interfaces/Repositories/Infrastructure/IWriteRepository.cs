using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HCE.Interfaces.Repositories
{
    public interface IWriteRepository<T> where T : class
    {
        void Add(T entity);
        void Add(List<T> entities);

        Task AddAsync(T entity);
        Task AddAsync(List<T> entities);

        void BulkInsert(List<T> entities);
        Task BulkInsertAsync(List<T> entities);

        T Update(T entity);
        void Update(List<T> entities);

        void BulkUpdate(List<T> entities);
        Task BulkUpdateAsync(List<T> entities);

        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> predicate);
    }
}
