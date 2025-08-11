using System;
using TeachersPortal.Api.Domain.Repositories.Core;

namespace TeachPortal.Domain.Repositories.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IUnitOfWorkContext _context;

        public UnitOfWork(IUnitOfWorkContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.Commit();
        }

        public async Task CommitAsync()
        {
            await _context.CommitAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void RollbackChanges()
        {
            _context.Rollback();            
        }

        public void TryCommit()
        {
            try
            {
                _context.Commit();
            }
            catch (Exception)
            {
                RollbackChanges();
                throw;
            }
        }
    }
}
