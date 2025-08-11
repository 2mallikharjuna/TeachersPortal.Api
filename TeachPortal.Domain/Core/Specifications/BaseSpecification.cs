using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.FetchStrategy;
using TeachersPortal.Api.Domain.Core.Specifications;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;
using TeachPortal.Domain.Core.Specifications;

namespace TeachersPortal.Api.Domain.Core.Specification
{
    //<see cref="http://blog.willbeattie.net/2011/02/specification-pattern-entity-framework.html"/>
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity> 
        where TEntity : BaseEntity, ISpecifiable
    {
        public IFetchStrategy<TEntity> FetchStrategy { get; set; }

        public Expression<Func<TEntity, bool>> Predicate { get; set; }

        public abstract bool IsSatisfiedBy(TEntity entity);

        public abstract Expression<Func<TEntity, bool>> ToExpression();
        
        public ISpecification<TEntity> And(ISpecification<TEntity> other)
        {
            return new AndSpecification<TEntity>(this, other);
        }
        public ISpecification<TEntity> AndAlso(ISpecification<TEntity> other)
        {
            return new AndAlsoSpecification<TEntity>(this, other);
        }
        public ISpecification<TEntity> AndNot(ISpecification<TEntity> other)
        {
            return new AndNotSpecification<TEntity>(this, other);
        }

        public ISpecification<TEntity> Or(ISpecification<TEntity> other)
        {
            return new OrSpecification<TEntity>(this, other);
        }

        public ISpecification<TEntity> Not()
        {
            return new NotSpecification<TEntity>(this);
        }

        public static BaseSpecification<TEntity> operator &(BaseSpecification<TEntity> spec1, BaseSpecification<TEntity> spec2)
        {
            return new AndSpecification<TEntity>(spec1, spec2);
        }

        public static BaseSpecification<TEntity> operator |(BaseSpecification<TEntity> spec1, BaseSpecification<TEntity> spec2)
        {
            return new OrSpecification<TEntity>(spec1, spec2);
        }

        public static BaseSpecification<TEntity> operator !(BaseSpecification<TEntity> spec)
        {
            return new NotSpecification<TEntity>(spec);
        }

        public TEntity SatisfyingEntityFrom(IQueryable<TEntity> query)
        {
            return SatisfyingEntitiesFrom(query).FirstOrDefault();
        }

        public IQueryable<TEntity> SatisfyingEntitiesFrom(IQueryable<TEntity> query)
        {
            return Predicate == null ? query : query.Where(Predicate);
        } 
    }
}
