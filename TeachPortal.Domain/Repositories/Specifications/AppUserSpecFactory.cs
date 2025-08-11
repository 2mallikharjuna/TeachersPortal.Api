using System;
using TeachPortal.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specifications;
using TeachersPortal.Api.Domain.Core.FetchStrategy;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Domain.Repositories.Specifications
{
    public static class AppUserSpecFactory
    {
        public static ISpecification<AppUser> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));
            }

            return new DirectSpecification<AppUser>(user => user.Username == username)
            {
                FetchStrategy = new GenericFetchStrategy<AppUser>().Include(s => s.Teacher)
            };
        }
    }
}
