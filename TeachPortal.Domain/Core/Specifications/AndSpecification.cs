using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Domain.Core.Specifications;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachersPortal.Api.Domain.Core.Specifications
{
    public class AndSpecification<TEntity> : CompositeSpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {
        public AndSpecification(ISpecification<TEntity> leftSpec, ISpecification<TEntity> rightSpec) : base(leftSpec, rightSpec)
        {           
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            if (Left.IsSatisfiedBy(entity))
            {
                return Right.IsSatisfiedBy(entity);
            }
            return false;
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {            
            return Left.ToExpression().And(Right.ToExpression());
        }
    }
}
