using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Sample.Core.Repositories
{
    public interface IUnitOfWork
    {
        DbContext DbContext { get; }

        Task<int> SaveAsync();

        int Save();

        IDbContextTransaction BeginTransaction();

        bool EnableTrackChanges();

        void DetectChanges();

        void EnableQueryAllTracking();

        void EnableQueryNoTracking();
    }
}
