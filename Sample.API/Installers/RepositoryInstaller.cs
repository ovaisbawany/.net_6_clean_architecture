using Sample.Core.Entities;
using Sample.Core.Repositories;
using Sample.Core.Repositories.Base;
using Sample.Infrastructure.Repositories;
using Sample.Infrastructure.Repositories.Base;

namespace Sample.API.Installers
{
    public static class RepositoryInstaller
    {
        public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAuditableRepository<SampleTest, long>, AuditableRepository<SampleTest, long>>();
        }
    }
}
