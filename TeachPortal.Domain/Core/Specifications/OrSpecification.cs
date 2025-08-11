using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachPortal.Domain.Core.Specifications;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachersPortal.Api.Domain.Core.Specifications
{
    public class OrSpecification<TEntity> : CompositeSpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {        
        public OrSpecification(ISpecification<TEntity> leftSpec, ISpecification<TEntity> rightSpec):base(leftSpec, rightSpec)
        {            
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {            
            return Left.IsSatisfiedBy(entity) || Right.IsSatisfiedBy(entity);
        }

        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return Left.ToExpression().Or(Right.ToExpression());
        }
    }
}
