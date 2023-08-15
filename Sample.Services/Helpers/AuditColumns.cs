using Sample.Core.Entities.Base;
using Sample.Core.Generic;

namespace Sample.Service.Helpers
{
    public class AuditColumns<TEntity, TKey> : IAuditColumns<TEntity, TKey>
    where TEntity : IAuditModel<TKey>, new()
    {
        private readonly IRequestInfo _requestInfo;
        public AuditColumns(IRequestInfo requestInfo)
        {
            _requestInfo = requestInfo;
        }
        public void SetAuditColumns(TEntity entity)
        {
            entity.CreatedBy = _requestInfo.UserId;
            entity.CreatedOn = DateTime.UtcNow;
            entity.LastModifiedBy = _requestInfo.UserId;
            entity.LastModifiedOn = DateTime.UtcNow;
            entity.IsDeleted = false;
        }

        public void SetAuditColumns(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedBy = _requestInfo.UserId;
                entity.CreatedOn = DateTime.UtcNow;
                entity.LastModifiedBy = _requestInfo.UserId;
                entity.LastModifiedOn = DateTime.UtcNow;
                entity.IsDeleted = false;
            }
        }
    }
}
