using System;
using TeachPortal.Domain.Entities;
using TeachersPortal.Api.Domain.Repositories.Core;

namespace TeachPortal.Domain.Repositories
{
    public interface IAppUserRepository : IRepository<AppUser, int>
    {
        Task<AppUser> GetByUsernameAsync(string username);
        Task<bool> ExistsByUsername(string username);
    }
}
