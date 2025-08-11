using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specification;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Domain.Core.Specifications
{
    /// <summary>
    /// Represents the specification that can be satisfied by the given object
    /// in any circumstance.
    /// </summary>
    /// <typeparam name="TEntity">The type of the object to which the specification is applied.</typeparam>
    public sealed class AnySpecification<TEntity> : BaseSpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {
        public override bool IsSatisfiedBy(TEntity resource)
        {
            return true;
        }
        #region Public Methods
        /// <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>        
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            bool result = true;
            return (TEntity t) => result;
        }
        #endregion
    }
}
