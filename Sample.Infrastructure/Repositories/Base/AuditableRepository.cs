using Sample.Core.Entities.Base;
using Sample.Core.Generic;
using Sample.Core.Repositories.Base;
using Sample.Infrastructure.Data;

namespace Sample.Infrastructure.Repositories.Base
{

    public class AuditableRepository<TEntity, TKey> : Repository<TEntity, TKey>, IAuditableRepository<TEntity, TKey>
        where TEntity : class, IAuditModel<TKey>
        where TKey : IEquatable<TKey>
    {

        public AuditableRepository(SampleContext context, IRequestInfo requestInfo)
    : base(context, requestInfo)
        {
        }
        public override Task<TEntity> Create(TEntity entity)
        {
            entity.CreatedBy = _requestInfo.UserId;
            entity.CreatedOn = DateTime.UtcNow;
            entity.LastModifiedBy = _requestInfo.UserId;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.IsDeleted = false;
            return base.Create(entity);
        }

        public override Task<IEnumerable<TEntity>> Create(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedBy = _requestInfo.UserId;
                entity.CreatedOn = DateTime.UtcNow;
                entity.LastModifiedBy = _requestInfo.UserId;
                entity.LastModifiedOn = DateTime.UtcNow;
                entity.IsDeleted = false;
            }
            return base.Create(entities);
        }

        public override Task<TEntity> Update(TEntity entity)
        {
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.LastModifiedBy = _requestInfo.UserId;
            return base.Update(entity);
        }

        public override Task<IEnumerable<TEntity>> Update(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.LastModifiedOn = DateTime.UtcNow;
                entity.LastModifiedBy = _requestInfo.UserId;
            }
            return base.Update(entities);
        }

        public async override Task Delete(TEntity entity)
        {
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.LastModifiedBy = _requestInfo.UserId;
            entity.IsDeleted = true;
            await base.Update(entity);
        }

        public async override Task Delete(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.LastModifiedOn = DateTime.UtcNow;
                entity.LastModifiedBy = _requestInfo.UserId;
                entity.IsDeleted = true;
            }
            await base.Update(entities.ToList());
        }

        public virtual async Task HardDelete(TEntity entity)
        {
            await base.Delete(entity);
        }

        public virtual async Task HardDelete(IList<TEntity> entities)
        {
            await base.Delete(entities);
        }
    }
}
