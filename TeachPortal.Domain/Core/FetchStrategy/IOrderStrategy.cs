namespace TeachersPortal.Api.Domain.Core.FetchStrategy
{
    public interface IOrderStrategy<TEntity>
    {
        IOrderedQueryable<TEntity> ApplyOrderBy(IQueryable<TEntity> query);
        IOrderedQueryable<TEntity> ApplyThenBy(IOrderedQueryable<TEntity> query);
    }
}
