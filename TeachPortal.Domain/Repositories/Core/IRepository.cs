using System;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachersPortal.Api.Domain.Repositories.Core
{
    public interface IRepository<TEntity, in TKey> where TEntity : BaseEntity
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        void Remove(TEntity entity);

        Task AddRangeAsync(IEnumerable<TEntity> entities);

        void RemoveRangeAsync(IEnumerable<TEntity> entities);

        TEntity Get(TKey key);

        Task<TEntity> GetAsync(TKey key);

        Task<ICollection<TEntity>> AllMatchingAsync(ISpecification<TEntity> specification, IQueryable<TEntity> entitySet = null);        

        Task<ICollection<TEntity>> AllMatchingQueryableAsync(ISpecification<TEntity> specification);

        Task<ICollection<TEntity>> GetAllAsync();
        
    }
}
