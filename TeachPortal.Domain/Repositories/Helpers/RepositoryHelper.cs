using System;
using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Core.FetchStrategy;
using TeachersPortal.Api.Domain.Entities; // Ensure this namespace is included for BaseEntity  

namespace TeachPortal.Domain.Repositories.Helpers
{
    public static class RepositoryHelper
    {
        public static IFetchStrategy<T> BuildFetchStrategy<T>(params string[] includePaths) where T : BaseEntity
        {
            var fetchStrategy = new GenericFetchStrategy<T>();
            foreach (var path in includePaths)
            {
                fetchStrategy.Include(path);
            }
            return fetchStrategy;
        }

        public static IFetchStrategy<T> BuildFetchStrategy<T>(params Expression<Func<T, object>>[] includePaths) where T : BaseEntity
        {
            var fetchStrategy = new GenericFetchStrategy<T>();
            foreach (var path in includePaths)
            {
                fetchStrategy.Include(path);
            }
            return fetchStrategy;
        }
    }
}

