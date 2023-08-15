using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Sample.Core.Generic
{
    public interface IQueryFilter<TEntity> 
    {
        IQueryFilter<TEntity> OrderBy(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderByExpression);

        IQueryFilter<TEntity> IncludeInCore(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> expression);

        Task<Tuple<int, IEnumerable<TEntity>>> SelectPageAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,  IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes = null, int? page = null, int? pageSize = null);

        IEnumerable<TResult> Select<TResult>(Expression<Func<TEntity, TResult>> selector = null);

        IEnumerable<TEntity> Select();

        Task<IEnumerable<TEntity>> SelectAsync();
    }
}
