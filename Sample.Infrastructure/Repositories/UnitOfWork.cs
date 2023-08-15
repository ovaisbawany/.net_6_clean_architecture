using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sample.Core.Repositories;
using Sample.Infrastructure.Data;

namespace Sample.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly SampleContext _context;
        public UnitOfWork(SampleContext context)
        {
            _context = context;
        }

        public DbContext DbContext
        {
            get
            {
                return _context;
            }
        }

        public int Save()
        {
            return DbContext.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public IDbContextTransaction BeginTransaction()
        {
            return this.DbContext.Database.BeginTransaction();
        }


        public bool EnableTrackChanges()
        {
            return this.DbContext.ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public void DetectChanges()
        {
            this.DbContext.ChangeTracker.DetectChanges();
        }

        public void EnableQueryNoTracking()
        {
            this.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void EnableQueryAllTracking()
        {
            this.DbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
        }
    }
}
