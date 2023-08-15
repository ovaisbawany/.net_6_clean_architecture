using Sample.Core.Entities;
using Sample.Core.Generic;
using Sample.Service.Helpers;

namespace Sample.API.Installers
{
    public static class HelperInstaller
    {
        public static void AddHelpers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRequestInfo, RequestInfo>();
            services.AddTransient<IAuditColumns<SampleTestChild, long>, AuditColumns<SampleTestChild, long>>();
        }
    }
}
