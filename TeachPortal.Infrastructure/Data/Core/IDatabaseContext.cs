using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;
using TeachersPortal.Api.Domain.Entities;

namespace TeachPortal.Infrastructure.Data.Core
{
    /// <summary>
    /// Provides an abstraction for database operations on entities.
    /// Supports synchronous and asynchronous CRUD, querying, and specification-based retrieval.
    /// </summary>
    public interface IDatabaseContext
    {
        /// <summary>
        /// Adds a new entity to the context.
        /// </summary>
        void AddEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Updates an existing entity in the context.
        /// </summary>
        void UpdateEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Deletes an entity from the context.
        /// </summary>
        void DeleteEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Retrieves an entity by its integer identifier.
        /// </summary>
        TEntity GetEntityById<TEntity>(int id) where TEntity : BaseEntity;

        /// <summary>
        /// Retrieves all entities of the specified type.
        /// </summary>
        IEnumerable<TEntity> GetAllEntities<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Finds entities matching the given predicate.
        /// </summary>
        IEnumerable<TEntity> FindEntities<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously retrieves an entity by its integer identifier.
        /// </summary>
        Task<TEntity> GetEntityByIdAsync<TEntity>(int id) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously retrieves all entities of the specified type.
        /// </summary>
        Task<IEnumerable<TEntity>> GetAllEntitiesAsync<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously finds entities matching the given predicate.
        /// </summary>
        Task<IEnumerable<TEntity>> FindEntitiesAsync<TEntity>(Func<TEntity, bool> predicate) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously adds a new entity to the context.
        /// </summary>
        Task AddEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously updates an existing entity in the context.
        /// </summary>
        Task UpdateEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously deletes an entity from the context.
        /// </summary>
        Task DeleteEntityAsync<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously checks if an entity exists by its integer identifier.
        /// </summary>
        Task<bool> EntityExistsAsync<TEntity>(int id) where TEntity : BaseEntity;

        /// <summary>
        /// Returns a queryable collection of entities.
        /// </summary>
        IQueryable<TEntity> GetQueryable<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Returns a queryable collection of entities (alias for GetQueryable).
        /// </summary>
        IQueryable<TEntity> GetQueryableEntity<TEntity>() where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously retrieves all entities matching a specification from a given entity set.
        /// </summary>
        Task<ICollection<TEntity>> GetAllMatchingAsync<TEntity>(ISpecification<TEntity> specification, IQueryable<TEntity> entitySet) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously retrieves all entities implementing ISpecifiable.
        /// </summary>
        Task<ICollection<TEntity>> AllEntitiesAsync<TEntity>() where TEntity : BaseEntity, ISpecifiable;

        /// <summary>
        /// Asynchronously checks if any entity matches the given specification.
        /// </summary>
        Task<bool> AnyAsync<TEntity>(ISpecification<TEntity> specification) where TEntity : BaseEntity;

        /// <summary>
        /// Removes an entity from the context.
        /// </summary>
        void RemoveEntity<TEntity>(TEntity entity) where TEntity : BaseEntity;

        /// <summary>
        /// Removes a range of entities from the context.
        /// </summary>
        void RemoveRangeEntity<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously retrieves an entity by its key.
        /// </summary>
        Task<TEntity> GetEntityAsync<TEntity>(object key);

        /// <summary>
        /// Retrieves an entity by its key.
        /// </summary>
        TEntity GetEntity<TEntity>(object key);

        /// <summary>
        /// Asynchronously adds a range of entities to the context.
        /// </summary>
        Task AddEntityRangeAsync<TEntity>(IEnumerable<TEntity> entities) where TEntity : BaseEntity;

        /// <summary>
        /// Asynchronously retrieves all entities matching a specification from a given entity set.
        /// </summary>
        Task<ICollection<TEntity>> GetAllEntitiesMatchingAsync<TEntity>(ISpecification<TEntity> specification, IQueryable<TEntity> entitySet) where TEntity : BaseEntity;

        /// <summary>
        /// Returns a queryable collection of entities matching a specification from a given entity set.
        /// </summary>
        IQueryable<TEntity> GetAllEntitiesMatching<TEntity>(ISpecification<TEntity> specification, IQueryable<TEntity> entitySet) where TEntity : BaseEntity;
    }
}
