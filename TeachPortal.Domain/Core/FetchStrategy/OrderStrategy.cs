using System.Linq.Expressions;

namespace TeachersPortal.Api.Domain.Core.FetchStrategy
{
    public class OrderStrategy<TEntity, TOrderValue> : IOrderStrategy<TEntity>
    {
        public bool Descending { get; set; }

        private readonly Expression<Func<TEntity, TOrderValue>> _orderValue;
        public OrderStrategy(Expression<Func<TEntity, TOrderValue>> orderValue)
            : this(orderValue, false)
        {
        }

        public IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query)
        {
            if (query != null)
            {
                return !Descending ? query.OrderBy(_orderValue) : query.OrderByDescending(_orderValue);
            }
            return null;
        }

        public IOrderedQueryable<TEntity> ApplyThenBy(IOrderedQueryable<TEntity> query)
        {
            if (query != null)
            {
                return !Descending ? query.ThenBy(_orderValue) : query.ThenByDescending(_orderValue);
            }
            return null;
        }

        public OrderStrategy(Expression<Func<TEntity, TOrderValue>> orderValue, bool descending)
        {
            Descending = descending;
            _orderValue = orderValue;
        }
    }
}
