using System;

namespace TeachPortal.Domain.Repositories.Core
{
    public interface IUnitOfWorkContext : IDisposable
    {
        /// <summary>
        /// Commit all changes.
        /// </summary>
        void Commit();

        /// <summary>
        /// Commit all changes asynchronously.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        /// Rollback all changes asynchronously.
        /// </summary>
        void Rollback();
    }
}
