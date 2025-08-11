using TeachPortal.Domain.Entities;
using TeachPortal.Domain.Repositories;
using TeachPortal.Domain.Repositories.Specifications;
using TeachPortal.Infrastructure.Data.Core;

namespace TeachPortal.Infrastructure.Data.Repositories
{
    public class AppUserRepository : BaseRepository<AppUser, int>, IAppUserRepository
    {
        protected AppUserRepository(IDatabaseContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> ExistsByUsername(string username)
        {
            var spec = AppUserSpecFactory.GetByUsernameAsync(username);
            return await AnyAsync(spec);
        }

        public async Task<AppUser> GetByUsernameAsync(string username)
        {
            var spec = AppUserSpecFactory.GetByUsernameAsync(username);
            var users = await AllMatchingAsync(spec);
            return users.FirstOrDefault();
        }
    }
}
