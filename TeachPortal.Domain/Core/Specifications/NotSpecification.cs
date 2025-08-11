using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specification;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachersPortal.Api.Domain.Core.Specifications
{
    public class NotSpecification<TEntity> : BaseSpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {
        ISpecification<TEntity> Candidate;
        public NotSpecification(ISpecification<TEntity> candidate)
        {
            Candidate = candidate;
        }
        public override bool IsSatisfiedBy(TEntity candidate)
        {
            return !Candidate.IsSatisfiedBy(candidate);
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var body = Expression.Not(this.Candidate.ToExpression().Body);
            return Expression.Lambda<Func<TEntity, bool>>(body, this.Candidate.ToExpression().Parameters);
        }
    }
}
