using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sample.Core.Entities.Base;
using Sample.Core.Generic;
using Sample.Core.Repositories.Base;
using Sample.Infrastructure.Data;
using System.Linq.Expressions;

namespace Sample.Infrastructure.Repositories.Base
{
    // Generic command repository class
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IBase<TKey>
        where TKey : IEquatable<TKey>
    {
        protected readonly SampleContext _context;
        protected readonly IRequestInfo _requestInfo;

        public Repository(SampleContext context, IRequestInfo requestInfo)
        {
            _context = context;
            _requestInfo = requestInfo;
        }

        public DbContext DbContext
        {
            get
            {
                return _context;
            }
        }
        public virtual async Task<TEntity> Create(TEntity entity)
        {
            await this.DbContext.AddAsync(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> Create(IEnumerable<TEntity> entities)
        {
            await this.DbContext.AddRangeAsync(entities);
            return entities;
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            this.DbContext.Update(entity);
            return entity;
        }

        public virtual async Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
        {
            this.DbContext.UpdateRange(entities);
            return entities;
        }

        public virtual async Task Delete(TEntity entity) => this.DbContext.Remove(entity);


        public virtual async Task Delete(IEnumerable<TEntity> entities)
        {
            this.DbContext.RemoveRange(entities);
        }

        protected virtual IQueryable<TEntity> DefaultListQuery => this.DbContext.Set<TEntity>().AsQueryable().OrderBy(x => x.Id);

        protected virtual IQueryable<TEntity> DefaultUnOrderedListQuery => this.DbContext.Set<TEntity>().AsQueryable();

        protected virtual IQueryable<TEntity> DefaultSingleQuery => this.DbContext.Set<TEntity>().AsQueryable();
        public virtual async Task<TEntity> GetAsync(TKey id) => await this.DefaultSingleQuery.FirstOrDefaultAsync(x => x.Id.Equals(id));

        public virtual async Task<IEnumerable<TEntity>> GetAsync(IList<TKey> ids) => await this.DefaultSingleQuery.Where(x => ids.Contains(x.Id)).ToListAsync();

        public virtual async Task<int> GetCount() => await this.DefaultListQuery.CountAsync();

        public virtual async Task<TEntity> GetEntityOnly(TKey id) => await this.DbContext.Set<TEntity>().AsQueryable().SingleOrDefaultAsync(x => x.Id.Equals(id));

        //Use includesInCore param in Ef Core when Includes within Includes needed
        public virtual async Task<TEntity> Find(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includesInCore = null)
        {
            IQueryable<TEntity> query = this.DefaultUnOrderedListQuery;
            query = Queryable.Where(query, filter);

            if (includesInCore != null)
            {
                query = includesInCore(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        //Use includesInCore param in Ef Core when Includes within Includes needed
        public virtual async Task<List<TEntity>> FindAll(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includesInCore = null)
        {
            IQueryable<TEntity> query = this.DefaultUnOrderedListQuery;
            query = Queryable.Where(query, filter);

            if (includesInCore != null)
            {
                query = includesInCore(query);
            }

            return await query.ToListAsync();
        }

        public virtual IQueryFilter<TEntity> Query(Expression<Func<TEntity, bool>> queryExpression) => new QueryFilter<TEntity, TKey>(this, queryExpression);

        internal async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeInCore = null,
            int? page = null, int? pageSize = null)
        {
            return (await GetPagedResultAsync(filter, orderBy, page, pageSize, includeInCore)).Item2;
        }

        /// <summary>
        /// GetMaxAsync
        /// </summary>
        /// <param name="selector"></param>
        /// /// <param name="filter"></param>
        /// <returns></returns>
        internal async Task<TResult> GetMaxAsync<TResult>(Expression<Func<TEntity, TResult>> selector,
          Expression<Func<TEntity, bool>> filter = null) where TResult : IEquatable<TResult>
        {
            IQueryable<TEntity> query = this.DefaultUnOrderedListQuery;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.MaxAsync(selector);
        }

        /// <summary>
        /// GetMax
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public virtual async Task<TResult> GetMax<TResult>(Expression<Func<TEntity, TResult>> selector) where TResult : IEquatable<TResult>
        {
            return await GetMaxAsync(selector, null);
        }

        /// <summary>
        /// GetPagedResultAsync
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// /// <param name="includeInCore"></param>
        /// <returns></returns>
        public virtual async Task<Tuple<int, IEnumerable<TEntity>>> GetPagedResultAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,
            IOrderedQueryable<TEntity>> orderBy = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includeInCore = null)
        {
            IQueryable<TEntity> query = this.DefaultUnOrderedListQuery;

            if (includeInCore != null)
            {
                query = includeInCore(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            int totalCount = await query.CountAsync();

            if (page != null)
            {
                query = query.Skip(((int)page - 1) * (int)pageSize);
            }

            if (pageSize != null)
            {
                query = query.Take((int)pageSize);
            }

            var data = await query.ToListAsync();
            return new Tuple<int, IEnumerable<TEntity>>(totalCount, data);
        }

        /// <summary>
        /// GetCustomEntityPagedResultAsync
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// /// <param name="includeInCore"></param>
        /// <returns></returns>
        public async Task<Tuple<int, IEnumerable<TEntityList>>> GetCustomEntityPagedResultAsync<TEntityList>(Expression<Func<TEntityList, bool>> filter = null,
            Func<IQueryable<TEntityList>,
            IOrderedQueryable<TEntityList>> orderBy = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntityList>, IIncludableQueryable<TEntityList, object>> includeInCore = null)
            where TEntityList : class, IBase<TKey>
        {
            IQueryable<TEntityList> query = this.DbContext.Set<TEntityList>().AsQueryable();

            if (includeInCore != null)
            {
                query = includeInCore(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }
            else
            {
                query = query.OrderBy(x => x.Id);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            int totalCount = await query.CountAsync();

            if (page != null)
            {
                query = query.Skip(((int)page - 1) * (int)pageSize);
            }

            if (pageSize != null)
            {
                query = query.Take((int)pageSize);
            }

            var data = await query.ToListAsync();
            return new Tuple<int, IEnumerable<TEntityList>>(totalCount, data);
        }

        /// <summary>
        /// GetCustomEntityPagedResultAsync
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// /// <param name="includeInCore"></param>
        /// <returns></returns>
        public async Task<Tuple<int, IEnumerable<TEntityList>>> GetCustomEntityPagedResultAsync<TEntityList>(IQueryable<TEntityList> query, Expression<Func<TEntityList, bool>> filter = null,
            Func<IQueryable<TEntityList>,
            IOrderedQueryable<TEntityList>> orderBy = null,
            int? page = null,
            int? pageSize = null,
            Func<IQueryable<TEntityList>, IIncludableQueryable<TEntityList, object>> includeInCore = null)
            where TEntityList : class
        {
            if (includeInCore != null)
            {
                query = includeInCore(query);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }
            int totalCount = await query.CountAsync();
            if (page != null)
            {
                query = query.Skip(((int)page - 1) * (int)pageSize);
            }

            if (pageSize != null)
            {
                query = query.Take((int)pageSize);
            }

            var data = await query.ToListAsync();
            return new Tuple<int, IEnumerable<TEntityList>>(totalCount, data);
        }
    }
}
