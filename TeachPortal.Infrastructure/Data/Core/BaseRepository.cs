using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories.Core;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Infrastructure.Data.Core
{
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseEntity
    {
        protected IDatabaseContext DbContext { get; set; }

        protected BaseRepository(IDatabaseContext dbContext)
        {
            DbContext = dbContext;
        }

        public TEntity Get(TKey key)
        {
            return DbContext.GetEntity<TEntity>(key);
        }

        public Task<TEntity> GetAsync(TKey key)
        {
            return DbContext.GetEntityAsync<TEntity>(key);
        }

        public void Add(TEntity entity)
        {
            DbContext.AddEntity(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await DbContext.AddEntityAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            if (typeof(BaseEntity).IsAssignableFrom(typeof(TEntity)))
            {
                await DbContext.AddEntityRangeAsync(entities.Cast<BaseEntity>());
            }
            else
            {
                throw new InvalidOperationException($"The type {typeof(TEntity).Name} must inherit from BaseEntity.");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            await DbContext.UpdateEntityAsync(entity);
            // After updating, fetch the updated entity from the database
            return await DbContext.GetEntityAsync<TEntity>(entity.Id);
        }

        public void Remove(TEntity entity)
        {
            DbContext.RemoveEntity(entity);
        }

        public void RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            DbContext.RemoveRangeEntity(entities);
        }

        public async Task<ICollection<TEntity>> AllMatchingAsync(ISpecification<TEntity> specification, IQueryable<TEntity> entitySet = null)
        {
            return await DbContext.GetAllEntitiesMatchingAsync(specification, entitySet);
        }
        public IQueryable<TEntity> AllMatching(ISpecification<TEntity> specification, IQueryable<TEntity> entitySet = null)
        {
            return DbContext.GetAllEntitiesMatching(specification, entitySet);
        }
        public async Task<ICollection<TEntity>> AllMatchingQueryableAsync(ISpecification<TEntity> specification)
        {
            var entitySet = DbContext.GetQueryable<TEntity>();
            return await AllMatchingAsync(specification, entitySet);
        }
        public async Task<ICollection<TEntity>> GetAllAsync()
        {
            return await DbContext.AllEntitiesAsync<TEntity>();
        }

        public async Task<bool> AnyAsync(ISpecification<TEntity> specification)
        {
            return await DbContext.AnyAsync(specification);
        }
    }
}
