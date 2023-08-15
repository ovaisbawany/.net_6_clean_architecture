using Sample.API.Properties;
using Sample.Core.Properties;
using System.Reflection;

namespace Sample.API.Installers
{
    /// <summary>
    /// Installs AutoMapper
    /// </summary>
    public static class AutomapperInstaller
    {
        /// <summary>
        /// Adds the mappers.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void AddMappers(this IServiceCollection services)
        {
            //To keep the scan small
            var includeAssemblies = new List<Assembly>
            {
                typeof(ApiLayerAnchor).Assembly,
                typeof(CoreLayerAnchor).Assembly
            };

            services.AddAutoMapper(includeAssemblies);
        }
    }
}
