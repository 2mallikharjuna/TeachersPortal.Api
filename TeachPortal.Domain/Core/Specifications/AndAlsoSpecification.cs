using System;
using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Domain.Core.Specifications
{
    public class AndAlsoSpecification<TEntity> : CompositeSpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {    
        public AndAlsoSpecification(ISpecification<TEntity> leftSpec, ISpecification<TEntity> rightSpec) : base(leftSpec, rightSpec) { }
                    
        // <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            //var body = Expression.AndAlso(Left.GetExpression().Body, Right.GetExpression().Body);
            //return Expression.Lambda<Func<T, bool>>(body, Left.GetExpression().Parameters);
            return Left.ToExpression().AndAlso(Right.ToExpression());
        }
    }
}
