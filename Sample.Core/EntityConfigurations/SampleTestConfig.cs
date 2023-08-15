using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities;

namespace Sample.Core.EntityConfigurations
{
    public class SampleTestConfig : BaseConfig<SampleTest>, IEntityTypeConfiguration<SampleTest>
    {
        public void Configure(EntityTypeBuilder<SampleTest> builder)
        {
            ConfigureCommonProperties(builder);
        }
    }
}
