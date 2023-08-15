using Microsoft.EntityFrameworkCore;
using Sample.Core.Entities;

namespace Sample.Infrastructure.Data.Seed
{
    public class SeedData
    {
        public void Seed(ModelBuilder modelBuilder)
        {
            var samples = new List<SampleTest>()
            {
                new SampleTest
                {
                    Id = 1,
                    Email = "integration.boomi@gmail.com",
                    FirstName = "Boomi",
                    LastName = "User",
                    Address = "integration.boomi",
                    ContactNumber = "123123123",
                    CreatedOn = new DateTime(2022, 01, 01)
                },
                new SampleTest
                {
                     Id = 2,
                     Email = "batch.job@gmail.com",
                     FirstName = "Batch",
                     LastName = "Job",
                     Address = "batch.job",
                     ContactNumber = "123123123",
                     CreatedOn = new DateTime(2022, 01, 01)
                }
            };
            modelBuilder.Entity<SampleTest>().HasData((IEnumerable<object>)samples);
        }
    }
}
