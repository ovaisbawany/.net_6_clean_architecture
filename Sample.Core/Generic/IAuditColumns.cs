namespace Sample.Core.Generic
{
    public interface IAuditColumns<TEntity, TKey>
    {
        void SetAuditColumns(TEntity entity);
        void SetAuditColumns(List<TEntity> entities);
    }
}
