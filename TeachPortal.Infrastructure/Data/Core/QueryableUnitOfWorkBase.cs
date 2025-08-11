using Microsoft.EntityFrameworkCore;
using TeachersPortal.Api.Domain.Entities;

namespace TeachPortal.Infrastructure.Data.Core
{
    public abstract class QueryableUnitOfWorkBase : DbContext, IQueryableUnitOfWork
    {
        public abstract void Commit();
        public abstract Task CommitAsync();
        public abstract void TryCommit();
        public abstract void CommitAndRefreshChanges();
        public abstract void RollbackChanges();

        #region Dispose
        public void DetachEntries<T>() where T : BaseEntity
        {
            try
            {
                // Remove the check for Database.GetDbConnection() since it's not available in EF Core by default.
                // Instead, just check if ChangeTracker is available and entries exist.
                if (ChangeTracker != null)
                {
                    // Update entries state
                    foreach (var entity in ChangeTracker.Entries<T>())
                        entity.State = EntityState.Detached;
                    DetachTrackerEntries();
                }
            }
            catch (Exception)
            {
                // Exception can be thrown when there is no active db context.
            }
        }

        private void DetachTrackerEntries()
        {
            foreach (var item in ChangeTracker.Entries())
                item.State = EntityState.Detached;
        }

        #endregion
    }
}
