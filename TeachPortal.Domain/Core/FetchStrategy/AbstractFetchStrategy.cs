using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Domain.Core.FetchStrategy;

namespace TeachersPortal.Api.Domain.Core.FetchStrategy
{
    public abstract class AbstractFetchStrategy<TEntity> : IFetchStrategy<TEntity> where TEntity : BaseEntity
    {
        // Existing properties
        public abstract IEnumerable<string> IncludePaths { get; }
        public abstract bool IsTracking { get; set; }
        public abstract IEnumerable<Expression<Func<TEntity, object>>> IncludeFuncs { get; }

        // Implemented interface properties
        public abstract IEnumerable<string> InternalPaths { get; }
        public abstract IEnumerable<IOrderStrategy<TEntity>> OrderStrategies { get; }
        public abstract IEnumerable<IWhereStrategy<TEntity>> WhereStrategies { get; }
        public abstract IPagingStrategy<TEntity> PagingStrategy { get; }
        public abstract IEnumerable<string> IncludeStringPaths { get; }
        public abstract IEnumerable<IIncludeDefinition> IncludeDefinitions { get; }
        public abstract int? MaxResults { get; set; }
        public abstract bool IsNotSplitQuery { get; set; }
        public abstract int? SkipRows { get; set; }
        public abstract int? TakeRows { get; set; }
        public abstract bool IsPagingEnabled { get; set; }

        // Existing abstract methods
        public abstract IFetchStrategy<TEntity> AsNoTracking();
        public abstract IFetchStrategy<TEntity> Include(Expression<Func<TEntity, object>> path);
        public abstract IFetchStrategy<TEntity> Include(string path);
        public abstract IFetchStrategy<TEntity> OrderBy<TOrderValueType>(Expression<Func<TEntity, TOrderValueType>> orderValue, bool descending = false);

        // Implemented interface methods
        public abstract IFetchStrategy<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        public abstract IFetchStrategy<TEntity> Take(int takeRows);
        public abstract IFetchStrategy<TEntity> Skip(int skipRows);
        public abstract IFetchStrategy<TEntity> Page(int takePageNumber, int pageSize = 0);

        public override string ToString()
        {
            return string.Format("Type: {0} Includes: {1}",
                    GetType().Name,
                    string.Join(",", IncludePaths)
                );
        }
    }
}
