using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Core.Entities;

namespace Sample.Core.EntityConfigurations
{
    public class SampleTestChildConfig : BaseConfig<SampleTestChild>, IEntityTypeConfiguration<SampleTestChild>
    {
        public void Configure(EntityTypeBuilder<SampleTestChild> builder)
        {
            ConfigureCommonProperties(builder);
        }
    }
}
