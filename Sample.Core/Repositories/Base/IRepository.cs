using Microsoft.EntityFrameworkCore.Query;
using Sample.Core.Entities.Base;
using Sample.Core.Generic;
using System.Linq.Expressions;

namespace Sample.Core.Repositories.Base
{

    public interface IRepository
    {
    }
    // Generic interface for command repository
    public interface IRepository<TEntity, TKey> : IRepository
    {
        Task<TEntity> Create(TEntity entity);

        Task<IEnumerable<TEntity>> Create(IEnumerable<TEntity> entities);

        Task<TEntity> Update(TEntity entity);

        Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities);

        Task Delete(TEntity entity);

        Task Delete(IEnumerable<TEntity> entities);

        // Generic repository for all if any
        Task<TEntity> GetAsync(TKey id);

        Task<IEnumerable<TEntity>> GetAsync(IList<TKey> ids);

        Task<TEntity> GetEntityOnly(TKey id);

        Task<int> GetCount();
        Task<TResult> GetMax<TResult>(Expression<Func<TEntity, TResult>> selector) where TResult : IEquatable<TResult>;


        Task<Tuple<int, IEnumerable<TEntity>>> GetPagedResultAsync(Expression<Func<TEntity, bool>> filter = null,
 Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
  int? page = null,
  int? pageSize = null,
  Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeInCore = null);

        Task<TEntity> Find(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>,
            IIncludableQueryable<TEntity, object>> includesInCore = null);

        Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includesInCore = null);

        IQueryFilter<TEntity> Query(Expression<Func<TEntity, bool>> queryExpression);

        Task<Tuple<int, IEnumerable<TEntityList>>> GetCustomEntityPagedResultAsync<TEntityList>(Expression<Func<TEntityList, bool>> filter = null,
            Func<IQueryable<TEntityList>,
            IOrderedQueryable<TEntityList>> orderBy = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntityList>, IIncludableQueryable<TEntityList, object>> includeInCore = null)
           where TEntityList : class, IBase<TKey>;

        Task<Tuple<int, IEnumerable<TEntityList>>> GetCustomEntityPagedResultAsync<TEntityList>(IQueryable<TEntityList> query,
            Expression<Func<TEntityList, bool>> filter = null,
            Func<IQueryable<TEntityList>,
            IOrderedQueryable<TEntityList>> orderBy = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntityList>, IIncludableQueryable<TEntityList, object>> includeInCore = null)
            where TEntityList : class;
    }
}
