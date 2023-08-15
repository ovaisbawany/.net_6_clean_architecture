using Sample.Core.DTOBase;
using Sample.Core.Repositories;

namespace Sample.Core.Services.Base
{
    public interface IService
    {
        IUnitOfWork UnitOfWork { get; }
    }

    public interface IService<TEntity, TKey> : IService
    {
        Task<IEnumerable<TKey>> GetIds();

        Task<int> GetCount();

        Task DeleteAsync(IList<TKey> ids);

        Task<IList<TEntity>> UpdateAsync(IList<TEntity> entities);

        Task BulkDeleteAsync(IList<TKey> ids);
    }

    public interface IService<TRepository, TEntity, TKey> : IService<TEntity, TKey>
    {
        TRepository Repository { get; }

        Task<DataTransferObject<List<TEntity>>> GetAllPagedAsync(DataTransferObject<TEntity> model);
    }
}
