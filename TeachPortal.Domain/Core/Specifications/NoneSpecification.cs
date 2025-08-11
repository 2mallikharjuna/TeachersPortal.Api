
using System.Linq.Expressions;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specification;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Domain.Core.Specifications
{
    public sealed class NoneSpecification<TEntity> : BaseSpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {
        public override bool IsSatisfiedBy(TEntity entity)
        {
            return false;
        }
        #region Public Methods
        /// <summary>
        /// Gets the LINQ expression which represents the current specification.
        /// </summary>
        /// <returns>The LINQ expression.</returns>
        public override Expression<Func<TEntity, bool>> ToExpression()
        {
            return o => false;
        }
        #endregion
    }
}
