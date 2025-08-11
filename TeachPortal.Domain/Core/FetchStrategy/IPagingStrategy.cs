using System;
using TeachersPortal.Api.Domain.Entities;

namespace TeachPortal.Domain.Core.FetchStrategy
{
    public interface IPagingStrategy<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query);
    }
}
