using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Eleven.Cloud.GCP.Repository.Base
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected IUnitOfWork _unitOfWork;

        public BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            if (entity == null) return null;

            _unitOfWork.Context.Set<T>().Add(entity);

            return await Task.FromResult(entity);
        }

        public virtual async Task<IEnumerable<T>> AddAsync(params T[] entities)
        {
            if (entities == null) return null;
            if (!entities.Any()) return entities;

            _unitOfWork.Context.Set<T>().AddRange(entities);

            return await Task.FromResult(entities);
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null) return null;

            _unitOfWork.Context.Set<T>().Attach(entity);
            _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
            _unitOfWork.Context.Update(entity);

            return await Task.FromResult(entity);
        }

        public virtual async Task<IEnumerable<T>> UpdateAsync(params T[] entities)
        {
            if (entities == null) return null;
            if (!entities.Any()) return entities;

            foreach (var entity in entities)
            {
                _unitOfWork.Context.Set<T>().Attach(entity);
                _unitOfWork.Context.Entry(entity).State = EntityState.Modified;
            }

            _unitOfWork.Context.Set<T>().UpdateRange(entities);

            return await Task.FromResult(entities);
        }

        public virtual async Task<int> DeleteAsync(T entity)
        {
            if (entity == null) return default;

            _unitOfWork.Context.Set<T>().Attach(entity);
            _unitOfWork.Context.Set<T>().Remove(entity);

            return await Task.FromResult(default(int));
        }

        public virtual async Task<int> DeleteRangeAsync(params T[] entities)
        {
            if (entities == null) return default;
            if (!entities.Any()) return default;

            entities.ToList().ForEach(entity => _unitOfWork.Context.Set<T>().Attach(entity));

            _unitOfWork.Context.Set<T>().RemoveRange(entities);

            return await Task.FromResult(default(int));
        }

        public virtual async Task<T> GetAsync(object key)
        {
            return await _unitOfWork.Context.Set<T>().FindAsync(key);
        }

        public virtual async Task<T> GetByAsync(Expression<Func<T, bool>> filter)
        {
            return await _unitOfWork.Context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public virtual async Task<T> GetByAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = FindAll().Where(filter);

            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return await queryable.FirstOrDefaultAsync();
        }

        public virtual IQueryable<T> FindAll()
        {
            return _unitOfWork.Context.Set<T>();
        }

        public virtual async Task<ICollection<T>> FindAllAsync()
        {
            return await _unitOfWork.Context.Set<T>().ToListAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _unitOfWork.Context.Set<T>().Where(filter);
            return query;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> filter)
        {
            return await _unitOfWork.Context.Set<T>().Where(filter).ToListAsync();
        }

        public virtual IQueryable<T> FindBy(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = FindAll().Where(filter);

            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return queryable;
        }

        public virtual async Task<ICollection<T>> FindByAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> queryable = FindAll().Where(filter);

            foreach (Expression<Func<T, object>> includeProperty in includeProperties)
            {
                queryable = queryable.Include(includeProperty);
            }

            return await queryable.ToListAsync();
        }

        public virtual async Task<int> SaveAsync()
        {
            return await _unitOfWork.Context.SaveChangesAsync();
        }
    }
}
