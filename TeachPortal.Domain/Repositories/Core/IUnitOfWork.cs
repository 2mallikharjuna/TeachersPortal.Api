namespace TeachersPortal.Api.Domain.Repositories.Core
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
        Task CommitAsync();
        void TryCommit();        
        void RollbackChanges();
    }
}
