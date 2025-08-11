using System;
using TeachersPortal.Api.Domain.Entities;

namespace TeachPortal.Domain.Core.FetchStrategy
{
    public class PagingStrategy<TEntity> : IPagingStrategy<TEntity> where TEntity : BaseEntity
    {
        public int PageNumber { get; }
        public int PageSize { get; }

        public PagingStrategy(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
                throw new ArgumentOutOfRangeException(nameof(pageNumber), "Page number must be greater than 0.");
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize), "Page size must be greater than 0.");

            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query)
        {
            return query
                .Skip((PageNumber - 1) * PageSize)
                .Take(PageSize);
        }
    }
}
