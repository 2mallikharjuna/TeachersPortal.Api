using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;

namespace TeachPortal.Domain.Core.FetchStrategy
{
    public interface IWhereStrategy<TEntity> where TEntity : BaseEntity
    {
        LambdaExpression Predicate { get; }

        IQueryable<TEntity> ApplyWhere(IQueryable<TEntity> query);
    }
}
