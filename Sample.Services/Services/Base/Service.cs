using AutoMapper;
using Sample.Core.DTOBase;
using Sample.Core.Entities.Base;
using Sample.Core.Repositories;
using Sample.Core.Repositories.Base;
using Sample.Core.Services.Base;

namespace Sample.Service.Services.Base
{
    public class Service : IService
    {
        public IUnitOfWork UnitOfWork { get; private set; }

        public Service(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
    public class Service<TRepository, TEntity, TKey> : Service, IService<TRepository, TEntity, TKey>
    where TEntity : IAuditModel<TKey>, new()
    where TRepository : IAuditableRepository<TEntity, TKey>
    where TKey : IEquatable<TKey>
    {
        protected readonly IMapper _mapper;

        public TRepository Repository => _repository;
        private readonly TRepository _repository;

        public Service(IUnitOfWork unitOfWork, TRepository repository, IMapper mapper)
            : base(unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public Service(IUnitOfWork unitOfWork, TRepository repository)
            : base(unitOfWork)
        {
            _repository = repository;
        }

        protected Task<TEntity> Create(TEntity entity)
        {
            return _repository.Create(entity);
        }

        internal virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await Create(entity);
            return result;
        }

        internal virtual async Task<IList<TEntity>> CreateAsync(IList<TEntity> entities)
        {
            List<TEntity> results = new List<TEntity>();

            foreach (TEntity entity in entities)
            {
                results.Add(await Create(entity));
            }
            UnitOfWork.DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            await UnitOfWork.SaveAsync();
            UnitOfWork.DbContext.ChangeTracker.AutoDetectChangesEnabled = true;

            return results;
        }

        protected async Task Delete(TKey id)
        {
            TEntity item = await _repository.GetAsync(id);
            await _repository.Delete(item);
        }

        protected async Task HardDelete(TKey id)
        {
            TEntity item = await _repository.GetAsync(id);
            await _repository.HardDelete(item);
        }

        internal virtual async Task DeleteAsync(TKey id)
        {
            await Delete(id);
        }

        public virtual async Task HardDeleteAsync(TKey id)
        {
            await HardDelete(id);
        }

        public virtual async Task DeleteAsync(IList<TKey> ids)
        {
            foreach (TKey id in ids)
            {
                await Delete(id);
            }

            UnitOfWork.DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            await UnitOfWork.SaveAsync();
            UnitOfWork.DbContext.ChangeTracker.AutoDetectChangesEnabled = true;
        }

        public virtual async Task<int> GetCount()
        {
            return await _repository.GetCount();
        }

        public async Task<IEnumerable<TKey>> GetIds() => (await GetAllAsync()).Select(s => s.Id);

        internal virtual async Task<List<TEntity>> GetAllAsync()
        {
            var results = await _repository.Query(x => true).SelectAsync();
            return results.ToList();
        }

        public async Task<DataTransferObject<List<TEntity>>> GetAllPagedAsync(DataTransferObject<TEntity> model)
        {
            var results = await _repository.GetPagedResultAsync(page: model.Paging.PageNumber, pageSize: model.Paging.PageSize);

            var collection = _mapper.Map<List<TEntity>>(results.Item2);
            model.Paging.TotalCount = results.Item1;

            return new DataTransferObject<List<TEntity>>(collection, model.Paging);
        }

        internal virtual async Task<TEntity> GetAsync(TKey id)
        {
            TEntity entity = await _repository.GetAsync(id);
            return entity;
        }

        protected async Task<TEntity> Update(TEntity entity)
        {
            return await _repository.Update(entity);
        }

        internal virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var result = await Update(entity);
            return result;
        }

        public virtual async Task<IList<TEntity>> UpdateAsync(IList<TEntity> entities)
        {
            List<Task> taskList = new List<Task>();

            foreach (var entityObject in entities)
            {
                taskList.Add(_repository.Update(entityObject));
            }

            await Task.WhenAll(taskList);

            UnitOfWork.DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
            await UnitOfWork.SaveAsync();
            UnitOfWork.DbContext.ChangeTracker.AutoDetectChangesEnabled = true;

            return entities;
        }

        public async Task BulkDeleteAsync(IList<TKey> ids)
        {
            foreach (TKey id in ids)
            {
                await Delete(id);
            }

            await UnitOfWork.SaveAsync();
        }

        public async Task BulkHardDeleteAsync(IList<TKey> ids)
        {
            foreach (TKey id in ids)
            {
                await HardDelete(id);
            }

            await UnitOfWork.SaveAsync();
        }

        protected virtual async Task SaveContext()
        {
            await UnitOfWork.SaveAsync();
        }
    }

}
