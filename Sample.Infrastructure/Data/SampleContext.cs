using Microsoft.EntityFrameworkCore;
using Sample.Core.Entities;
using Sample.Core.EntityConfigurations;
using Sample.Infrastructure.Data.Seed;

namespace Sample.Infrastructure.Data
{
    // Context class for command
    public class SampleContext : DbContext
    {
        public SampleContext(DbContextOptions<SampleContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedData seedData = new SeedData();
            seedData.Seed(modelBuilder);
            EntityConfigs(modelBuilder);
        }

        public DbSet<SampleTest> SampleTest { get; set; }
        public DbSet<SampleTestChild> SampleTestChild { get; set; }


        private void EntityConfigs(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.ApplyConfiguration(new SampleTestConfig());
            modelBuilder.ApplyConfiguration(new SampleTestChildConfig());
        }
    }
}
