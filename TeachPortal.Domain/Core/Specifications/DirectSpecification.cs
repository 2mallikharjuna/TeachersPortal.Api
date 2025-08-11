using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specification;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachersPortal.Api.Domain.Core.Specifications
{
    /// <summary>
    /// Represents the specification which is represented by the corresponding
    /// LINQ expression.
    /// </summary>
    /// <typeparam name="TEntity">The type of the object to which the specification is applied.</typeparam>
    public sealed class DirectSpecification<TEntity>
        : BaseSpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {
        #region Private Fields
        Expression<Func<TEntity, bool>> _matchingCriteria;        
        #endregion

        /// <summary>
        /// Initializes a new instance of <c>ExpressionSpecification&lt;T&gt;</c> class.
        /// </summary>
        /// <param name="expression">The LINQ expression which represents the current
        /// specification.</param>
        public DirectSpecification(Expression<Func<TEntity, bool>> matchingCriteria)
        {
            if (matchingCriteria == (Expression<Func<TEntity, bool>>)null)
                throw new ArgumentNullException("matchingCriteria");
           
            _matchingCriteria = matchingCriteria;
        }
        /// <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression satisfy condition.</returns>

        public override bool IsSatisfiedBy(TEntity entity)
        {
            return ToExpression().Compile()(entity);
        }
        /// <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return _matchingCriteria;
        }

    }
}
