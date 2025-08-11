using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Domain.Core.FetchStrategy;

namespace TeachersPortal.Api.Domain.Core.FetchStrategy
{
    public interface IFetchStrategy<TEntity> where TEntity : BaseEntity    
    {
        IEnumerable<string> InternalPaths { get; }

        IEnumerable<IOrderStrategy<TEntity>> OrderStrategies { get; }

        IEnumerable<IWhereStrategy<TEntity>> WhereStrategies { get; }

        IPagingStrategy<TEntity> PagingStrategy { get; }

        IEnumerable<string> IncludeStringPaths { get; }

        IEnumerable<IIncludeDefinition> IncludeDefinitions { get; }

        bool IsTracking { get; set; }

        int? MaxResults { get; set; }

        bool IsNotSplitQuery { get; set; }

        int? SkipRows { get; set; }

        int? TakeRows { get; set; }

        bool IsPagingEnabled { get; set; }

        IFetchStrategy<TEntity> OrderBy<TOrderValueType>(Expression<Func<TEntity, TOrderValueType>> orderValue, bool descending = false);

        IFetchStrategy<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        IFetchStrategy<TEntity> Include(string path);

        IFetchStrategy<TEntity> Take(int takeRows);

        IFetchStrategy<TEntity> Skip(int skipRows);

        IFetchStrategy<TEntity> Page(int takePageNumber, int pageSize = 0);
    }
}
