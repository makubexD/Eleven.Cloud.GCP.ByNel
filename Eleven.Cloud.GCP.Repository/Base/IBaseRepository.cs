using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eleven.Cloud.GCP.Repository.Base
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddAsync(params T[] entities);
        Task<T> UpdateAsync(T entity);
        Task<IEnumerable<T>> UpdateAsync(params T[] entities);
        Task<int> DeleteAsync(T entity);
        Task<int> DeleteRangeAsync(params T[] entities);
        Task<T> GetAsync(object key);
        Task<T> GetByAsync(Expression<Func<T, bool>> filter);
        Task<T> GetByAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        IQueryable<T> FindAll();
        Task<ICollection<T>> FindAllAsync();
        IQueryable<T> FindBy(Expression<Func<T, bool>> filter);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> filter);
        IQueryable<T> FindBy(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties);
        Task<int> SaveAsync();
    }
}
