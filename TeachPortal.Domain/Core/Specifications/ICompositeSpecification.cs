using System;
using TeachersPortal.Api.Domain.Core.Specifications.Interfaces;
using TeachersPortal.Api.Domain.Entities;

namespace TeachPortal.Domain.Core.Specifications
{
    public interface ICompositeSpecification<TEntity> : ISpecification<TEntity> where TEntity : BaseEntity, ISpecifiable
    {
        /// <summary>
        /// Gets the left side of the specification.
        /// </summary>
        ISpecification<TEntity> Left { get; }
        /// <summary>
        /// Gets the right side of the specification.
        /// </summary>
        ISpecification<TEntity> Right { get; }
    }
}
