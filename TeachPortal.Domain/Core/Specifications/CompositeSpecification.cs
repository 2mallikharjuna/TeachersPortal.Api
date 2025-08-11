using System;
using TeachersPortal.Api.Domain.Entities;
using TeachersPortal.Api.Domain.Core.Specification;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;

namespace TeachPortal.Domain.Core.Specifications
{
    /// <summary>
    /// Represents the base class for composite specifications.
    /// </summary>
    /// <typeparam name="T">The type of the object to which the specification is applied.</typeparam>
    public abstract class CompositeSpecification<TEntity> : BaseSpecification<TEntity>, ICompositeSpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {
        #region Private Fields
        private readonly ISpecification<TEntity> left;
        private readonly ISpecification<TEntity> right;
        #endregion

        #region Ctor
        /// <summary>
        /// Constructs a new instance of <c>CompositeSpecification&lt;T&gt;</c> class.
        /// </summary>
        /// <param name="left">The first specification.</param>
        /// <param name="right">The second specification.</param>
        public CompositeSpecification(ISpecification<TEntity> leftSpec, ISpecification<TEntity> rightSpec)
        {
            if (leftSpec == null)
            {
                throw new ArgumentNullException("leftSpec");
            }

            if (rightSpec == null)
            {
                throw new ArgumentNullException("rightSpec");
            }
            this.left = leftSpec;
            this.right = rightSpec;
        }
        #endregion

        #region ICompositeSpecification Members
        /// <summary>
        /// Gets the first specification.
        /// </summary>
        public ISpecification<TEntity> Left
        {
            get { return this.left; }
        }
        /// <summary>
        /// Gets the second specification.
        /// </summary>
        public ISpecification<TEntity> Right
        {
            get { return this.right; }
        }

        public override bool IsSatisfiedBy(TEntity entity)
        {
            if (Left.IsSatisfiedBy(entity))
            {
                return Right.IsSatisfiedBy(entity);
            }

            return false;
        }
        #endregion
    }
}
