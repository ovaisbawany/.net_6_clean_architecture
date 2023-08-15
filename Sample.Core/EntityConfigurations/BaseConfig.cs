using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sample.Core.EntityConfigurations
{
    public abstract class BaseConfig<TEntity>
        where TEntity : class
    {
        public void ConfigureCommonProperties(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasQueryFilter(m => !EF.Property<bool>(m, "IsDeleted"));
        }
    }
}
