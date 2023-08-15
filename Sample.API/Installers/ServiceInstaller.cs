using Sample.Core.Services;
using Sample.Core.Services.Base;
using Sample.Service.Services;

namespace Sample.API.Installers
{
    public static class ServiceInstaller
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IService, Service.Services.Base.Service>();
            services.AddScoped<ISampleService, SampleService>();
        }
    }
}
