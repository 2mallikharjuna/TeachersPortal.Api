using System;
using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Domain.Core.Specifications
{
    public class AndNotSpecification<TEntity> : CompositeSpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {
        public AndNotSpecification(ISpecification<TEntity> leftSpec, ISpecification<TEntity> rightSpec) : base(leftSpec, rightSpec) { }
                
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            var bodyNot = Expression.Not(Right.ToExpression().Body);
            var bodyNotExpression = Expression.Lambda<Func<TEntity, bool>>(bodyNot, Right.ToExpression().Parameters);

            return Left.ToExpression().AndNot(bodyNotExpression);
        }
    }
}
