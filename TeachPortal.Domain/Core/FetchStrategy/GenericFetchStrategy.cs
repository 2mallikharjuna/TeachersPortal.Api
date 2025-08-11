using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Domain.Core.FetchStrategy;

namespace TeachersPortal.Api.Domain.Core.FetchStrategy
{
    public class GenericFetchStrategy<TEntity> : AbstractFetchStrategy<TEntity> where TEntity : BaseEntity
    {
        private bool _isTracking;
        private bool _isPagingEnabled;
        private bool _isNotSplitQuery;
        private int? _maxResults;
        private int? _skipRows;
        private int? _takeRows;
        private readonly List<IOrderStrategy<TEntity>> _orderStrategies;
        private readonly List<IWhereStrategy<TEntity>> _whereStrategies;
        private IPagingStrategy<TEntity>? _pagingStrategy;
        private readonly IList<string> _properties;
        private readonly List<Expression<Func<TEntity, object>>> _includeFuncs;
        private readonly List<IIncludeDefinition> _includeDefinitions;

        public override bool IsTracking
        {
            get => _isTracking;
            set => _isTracking = value;
        }

        public override bool IsPagingEnabled
        {
            get => _isPagingEnabled;
            set => _isPagingEnabled = value;
        }

        public override bool IsNotSplitQuery
        {
            get => _isNotSplitQuery;
            set => _isNotSplitQuery = value;
        }

        public override int? MaxResults
        {
            get => _maxResults;
            set => _maxResults = value;
        }

        public override int? SkipRows
        {
            get => _skipRows;
            set => _skipRows = value;
        }

        public override int? TakeRows
        {
            get => _takeRows;
            set => _takeRows = value;
        }

        public override IEnumerable<string> IncludePaths => _properties;

        public override IEnumerable<string> InternalPaths => _properties;

        public override IEnumerable<string> IncludeStringPaths => _properties;

        public override IEnumerable<Expression<Func<TEntity, object>>> IncludeFuncs => _includeFuncs;

        public override IEnumerable<IOrderStrategy<TEntity>> OrderStrategies => _orderStrategies;

        public override IEnumerable<IWhereStrategy<TEntity>> WhereStrategies => _whereStrategies;

        public override IPagingStrategy<TEntity> PagingStrategy => _pagingStrategy!;

        public override IEnumerable<IIncludeDefinition> IncludeDefinitions => _includeDefinitions;

        public GenericFetchStrategy()
        {
            _includeFuncs = new List<Expression<Func<TEntity, object>>>();
            _properties = new List<string>();
            _isTracking = true;
            _orderStrategies = new List<IOrderStrategy<TEntity>>();
            _whereStrategies = new List<IWhereStrategy<TEntity>>();
            _includeDefinitions = new List<IIncludeDefinition>();
            _isPagingEnabled = false;
            _isNotSplitQuery = false;
            _maxResults = null;
            _skipRows = null;
            _takeRows = null;
            _pagingStrategy = null;
        }

        public override IFetchStrategy<TEntity> AsNoTracking()
        {
            _isTracking = false;
            return this;
        }

        public override IFetchStrategy<TEntity> Include(Expression<Func<TEntity, object>> path)
        {
            _includeFuncs.Add(path);
            _properties.Add(path.ToIncludeString());
            return this;
        }

        public override IFetchStrategy<TEntity> Include(string path)
        {
            _properties.Add(path);
            return this;
        }

        public override IFetchStrategy<TEntity> OrderBy<TOrderValueType>(Expression<Func<TEntity, TOrderValueType>> orderValue, bool descending = false)
        {
            _orderStrategies.Add(new OrderStrategy<TEntity, TOrderValueType>(orderValue, descending));
            return this;
        }

        public override IFetchStrategy<TEntity> Where(Expression<Func<TEntity, bool>> predicate)
        {
            _whereStrategies.Add(new WhereStrategy<TEntity>(predicate));
            return this;
        }

        public override IFetchStrategy<TEntity> Take(int takeRows)
        {
            _takeRows = takeRows;
            return this;
        }

        public override IFetchStrategy<TEntity> Skip(int skipRows)
        {
            _skipRows = skipRows;
            return this;
        }

        public override IFetchStrategy<TEntity> Page(int takePageNumber, int pageSize = 0)
        {
            _isPagingEnabled = true;
            _pagingStrategy = new PagingStrategy<TEntity>(takePageNumber, pageSize);
            return this;
        }
    }
}
