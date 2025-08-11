using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.FetchStrategy;

namespace TeachersPortal.Api.Domain.Core.Specifications.Interfaces
{
    public interface ISpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {
        IFetchStrategy<TEntity> FetchStrategy { get; set; }

        bool IsSatisfiedBy(TEntity entity);

        Expression<Func<TEntity, bool>> ToExpression();
        ISpecification<TEntity> And(ISpecification<TEntity> other);
        ISpecification<TEntity> AndAlso(ISpecification<TEntity> other);
        ISpecification<TEntity> AndNot(ISpecification<TEntity> other);
        ISpecification<TEntity> Or(ISpecification<TEntity> other);
        ISpecification<TEntity> Not();
    }
}
