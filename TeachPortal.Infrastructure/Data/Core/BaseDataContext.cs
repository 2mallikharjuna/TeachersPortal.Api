using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Domain.Repositories.Core;
using TeachersPortal.Api.Domain.Core.FetchStrategy;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Infrastructure.Data.Core
{
    public abstract class BaseDataContext : DbContext, IDatabaseContext, IUnitOfWorkContext
    {
        private readonly string _connectionString;
        protected BaseDataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public BaseDataContext(DbContextOptions options) : base(options)
        {
        }
        #region private methods
        private IQueryable<TEntity> AllMatchingQuery<TEntity>(ISpecification<TEntity> specification, IQueryable<TEntity> entitySet = null, bool isQuery = false)
            where TEntity : BaseEntity, ISpecifiable
        {
            var fs = specification.FetchStrategy;
            var query = GetQuery(fs, entitySet, isQuery).Where(specification.ToExpression());
            query = ApplyMaxResults(query, fs);// Apply max results if applicable
            return query;
        }
        private IQueryable<TEntity> ApplyMaxResults<TEntity>(IQueryable<TEntity> query,
            IFetchStrategy<TEntity> fetchStrategy) where TEntity : BaseEntity
        {
            int? maxResults = fetchStrategy?.MaxResults;
            if (maxResults.HasValue)
            {
                return query.Take(maxResults.Value);
            }

            return query;
        }

        private IQueryable<TEntity> GetQuery<TEntity>(IFetchStrategy<TEntity> fetchStrategy, IQueryable<TEntity> entitySet = null, bool isQuery = false)
            where TEntity : BaseEntity, ISpecifiable
        {
            IQueryable<TEntity> query;

            if (isQuery)
                query = entitySet == null ? Set<TEntity>().AsQueryable() : entitySet;
            else
                query = entitySet == null ? Set<TEntity>() : entitySet;

            if (fetchStrategy == null)
            {
                return query;
            }

            // Legacy to support previous string includes. Avoid using this.
            query = fetchStrategy.IncludeStringPaths.Aggregate(query, (current, path) =>
                current.Include(path));

            // Applies Includes/ThenIncludes
            foreach (var include in fetchStrategy.IncludeDefinitions)
            {
                var currentQuery = query.Include(Expression.Lambda<Func<TEntity, object>>(include.IncludeExpression.Body, include.IncludeExpression.Parameters));

                foreach (var thenInclude in include.ThenIncludeExpressions)
                {
                    var convertedExpression = include.VisitThenInclude(thenInclude);
                    currentQuery = currentQuery.ThenInclude(Expression.Lambda<Func<object, object>>(convertedExpression.Item1, convertedExpression.Item2));
                }

                query = currentQuery;
            }

            // Apply where if applicable
            query = ApplyWhereStrategies(query, fetchStrategy);

            // Apply order by if applicable
            query = ApplyOrderStrategies(query, fetchStrategy);

            // Apply skip/take strategy if applicable
            query = ApplySkipTakeStrategy(query, fetchStrategy);

            // Apply paging strategy if applicable
            query = ApplyPagingStrategy(query, fetchStrategy);

            query = fetchStrategy.IsNotSplitQuery ? query : query.AsSplitQuery();

            // Return query
            return fetchStrategy.IsTracking ? query : query.AsNoTracking();
        }

        private IQueryable<TEntity> ApplyPagingStrategy<TEntity>(IQueryable<TEntity> query, IFetchStrategy<TEntity> fetchStrategy)
            where TEntity : BaseEntity, ISpecifiable
        {
            if (!fetchStrategy.IsPagingEnabled || fetchStrategy.PagingStrategy == null) return query;

            return fetchStrategy.PagingStrategy.ApplyPaging(query);
        }

        private IQueryable<TEntity> ApplySkipTakeStrategy<TEntity>(IQueryable<TEntity> query, IFetchStrategy<TEntity> fetchStrategy)
            where TEntity : BaseEntity, ISpecifiable
        {
            if (fetchStrategy.SkipRows.HasValue)
            {
                query = query.Skip(fetchStrategy.SkipRows.Value);
            }

            if (fetchStrategy.TakeRows.HasValue)
            {
                query = query.Take(fetchStrategy.TakeRows.Value);
            }

            return query;
        }

        private IQueryable<TEntity> ApplyOrderStrategies<TEntity>(IQueryable<TEntity> query, IFetchStrategy<TEntity> fetchStrategy)
            where TEntity : BaseEntity, ISpecifiable
        {
            /* Apply order by if applicable */
            if (fetchStrategy.OrderStrategies.Any())
            {
                IOrderedQueryable<TEntity> orderedQueryable = null;
                var cnt = 0;
                foreach (var order in fetchStrategy.OrderStrategies)
                {
                    orderedQueryable = cnt == 0 ? order.ApplyOrderBy(query) : order.ApplyThenBy(orderedQueryable);
                    cnt++;
                }
                if (orderedQueryable != null)
                {
                    query = orderedQueryable;
                }
            }
            return query;
        }

        private IQueryable<TEntity> ApplyWhereStrategies<TEntity>(IQueryable<TEntity> query, IFetchStrategy<TEntity> fetchStrategy)
            where TEntity : BaseEntity, ISpecifiable
        {
            if (!fetchStrategy.WhereStrategies.Any()) return query;

            foreach (var whereStrat in fetchStrategy.WhereStrategies)
            {
                query = whereStrat.ApplyWhere(query);
            }

            return query;
        }
        #endregion


        public void AddEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Set<TEntity>().Add(entity);
        }

        public async Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await Set<TEntity>().AddAsync(entity);
        }

        public async Task AddEntityRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            await Set<TEntity>().AddRangeAsync(entities);
        }

        public async Task<ICollection<TEntity>> AllEntitiesAsync<TEntity>() where TEntity : BaseEntity, ISpecifiable
        {
            return await Set<TEntity>().ToListAsync();
        }


        public async Task<bool> AnyAsync<TEntity>(ISpecification<TEntity> specification) where TEntity : BaseEntity
        {
            var query = AllMatchingQuery(specification); // Ensure the correct generic type is passed  
            return await query.AnyAsync();
        }

        private IQueryable<TEntity> AllMatchingQuery<TEntity>(ISpecification<TEntity> specification) where TEntity : BaseEntity
        {
            // Corrected method signature to return IQueryable<TEntity> instead of object  
            return AllMatchingQuery(specification, null, false);
        }

        public void Commit() => SaveChanges();

        public void DeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Set<TEntity>().Remove(entity);
        }

        public async Task DeleteEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await Task.Run(() => Set<TEntity>().Remove(entity));
        }

        public async Task<bool> EntityExistsAsync<TEntity>(int id) where TEntity : BaseEntity
        {
            return await Set<TEntity>().AnyAsync(e => e.Id == id);
        }

        public IEnumerable<TEntity> FindEntities<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseEntity
        {
            return Set<TEntity>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<TEntity>> FindEntitiesAsync<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseEntity
        {
            return await Task.Run(() => Set<TEntity>().Where(predicate).ToList());
        }

        public IEnumerable<TEntity> GetAllEntities<TEntity>() where TEntity : BaseEntity
        {
            return Set<TEntity>().ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllEntitiesAsync<TEntity>() where TEntity : BaseEntity
        {
            return await Set<TEntity>().ToListAsync();
        }

        public Task<ICollection<TEntity>> GetAllEntitiesMatchingAsync<TEntity>(ISpecification<TEntity> specification, IQueryable<TEntity> entitySet) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<TEntity>> GetAllMatchingAsync<TEntity>(ISpecification<TEntity> specification, IQueryable<TEntity> entitySet) where TEntity : BaseEntity
        {
            throw new NotImplementedException();
        }

        public TEntity GetEntity<TEntity>(object key)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetEntityAsync<TEntity>(object key)
        {
            throw new NotImplementedException();
        }

        public TEntity GetEntityById<TEntity>(int id) where TEntity : BaseEntity
        {
            return Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetEntityByIdAsync<TEntity>(int id) where TEntity : BaseEntity
        {
            return await Set<TEntity>().FindAsync(id);
        }

        public IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : BaseEntity
        {
            return Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> GetQueryableEntity<TEntity>() where TEntity : BaseEntity
        {
            IQueryable<TEntity> queryableEntity = Set<TEntity>();
            return queryableEntity;
        }

        public void RemoveEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Remove(entity);
        }

        public void RemoveRangeEntity<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity
        {
            RemoveRange(entities);
        }

        public void RollbackChanges()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public void TryCommit()
        {
            try
            {
                SaveChanges();
            }
            catch
            {
                RollbackChanges();
                throw;
            }
        }

        public void UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            Set<TEntity>().Update(entity);
        }

        public async Task UpdateEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await Task.Run(() => Set<TEntity>().Update(entity));
        }

        /// <summary>
        /// Find all matching
        /// </summary>
        /// <param name="specification"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllEntitiesMatching<TEntity>(ISpecification<TEntity> specification, IQueryable<TEntity>? entitySet = null)
                where TEntity : BaseEntity
        {
            var query = AllMatchingQuery(specification, entitySet);
            return query;
        }

        public async Task CommitAsync()
        {
            await SaveChangesAsync();
        }

        public void Rollback()
        {
            RollbackChanges();
        }
    }
}
