
using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;

namespace TeachPortal.Domain.Core.FetchStrategy
{
    public class WhereStrategy<TEntity> : IWhereStrategy<TEntity> where TEntity : BaseEntity
    {
        public LambdaExpression Predicate { get; }

        public WhereStrategy(Expression<Func<TEntity, bool>> predicate)
        {
            Predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
        }

        public IQueryable<TEntity> ApplyWhere(IQueryable<TEntity> query)
        {
            // Apply the predicate to the query
            return query.Where((Expression<Func<TEntity, bool>>)Predicate);
        }
    }
}
